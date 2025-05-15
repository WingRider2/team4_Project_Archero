using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

/// <summary>
/// 몬스터에게 부여되는 상태이상(디버프)를 관리하는 클래스입니다.
/// 각 디버프는 Coroutine을 통해서 1초 단위로 데미지 주고, 같은 디버프가 들어오면 기존 지속시간을 초기화합니다.
/// </summary>
public class StatusEffectManager : MonoBehaviour
{
    // 현재 적용 중인 디버프와 해당 디버프의 코루틴을 저장하는 딕셔너리
    private Dictionary<DebuffType, Coroutine> activeDebuffs = new Dictionary<DebuffType, Coroutine>();
    private MonsterBase monster;

    private Dictionary<DebuffType, IDebuffSkill> debuffSkills = new Dictionary<DebuffType, IDebuffSkill>();

    void Start()
    {
        monster = GetComponent<MonsterBase>();
    }

    public void ApplyDebuff(IDebuffSkill debuffSkill)
    {
        if (debuffSkill.DebuffType == DebuffType.None) return;
        if (debuffSkills.TryGetValue(debuffSkill.DebuffType, out var debuff))
        {
            debuff.Duration = debuffSkill.Duration;
        }
        else
        {
            debuffSkills[debuffSkill.DebuffType] = debuffSkill;
            StartCoroutine(HandleDebuff(debuffSkills[debuffSkill.DebuffType]));
        }
    }

    private IEnumerator HandleDebuff(IDebuffSkill debuffSkill)
    {
        switch (debuffSkill.DebuffType)
        {
            case DebuffType.Burn:
            case DebuffType.Posion:
                {
                    while (debuffSkill.Duration > 0)
                    {
                        monster.Damaged(debuffSkill.DPS);

                        if (monster.IsDead)
                        {
                            yield break;
                        }

                        yield return new WaitForSeconds(1f);
                        debuffSkill.Duration -= 1f;
                    }

                    break;
                }
            case DebuffType.Slow:
                {
                    float originalMoveSpeed = monster.MonsterStatManager.GetFinalValue(StatType.MoveSpeed);
                    float reducedSpeed      = originalMoveSpeed * (1 - debuffSkill.DPS); // ex) value가 0.3이면 30% 감소

                    monster.MonsterStatManager.ApplyStatEffect(StatType.MoveSpeed, StatValueType.Buff, -(originalMoveSpeed - reducedSpeed));

                    while (debuffSkill.Duration > 0)
                    {
                        yield return new WaitForSeconds(1f);
                        debuffSkill.Duration -= 1f;
                    }

                    monster.MonsterStatManager.ApplyStatEffect(StatType.MoveSpeed, StatValueType.Buff, originalMoveSpeed - reducedSpeed);
                    break;
                }
        }

        debuffSkills.Remove(debuffSkill.DebuffType);
    }

    // 몬스터가 죽을때 호출되는 클리어 디버프 함수
    public void ClearAllDebuffs()
    {
        foreach (var pair in activeDebuffs)
        {
            StopCoroutine(pair.Value);
        }

        debuffSkills.Clear();
        activeDebuffs.Clear();
    }
}