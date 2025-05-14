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

    public void ApplyDebuff(DebuffType type, float DPS, float duration)
    {
        if (type == DebuffType.None) return;

        if (activeDebuffs.TryGetValue(type, out Coroutine exist))
        {
            StopCoroutine(exist);
        }

        Coroutine newDebuff = StartCoroutine(HandleDebuff(type, DPS, duration));
        activeDebuffs[type] = newDebuff;
    }

    public void ApplyDebuff(IDebuffSkill debuffSkill)
    {
        if (debuffSkill.DebuffType == DebuffType.None) return;
        if (debuffSkills.TryGetValue(debuffSkill.DebuffType, out var debuff))
        {
            Debug.Log("Duration 갱신");
            debuff.Duration = debuffSkill.Duration;
        }
        else
        {
            debuffSkills[debuffSkill.DebuffType] = debuffSkill;
            StartCoroutine(HandleDebuff(debuffSkills[debuffSkill.DebuffType]));
        }
    }

    private IEnumerator HandleDebuff(DebuffType type, float value, float duration)
    {
        float elapsed = 0f;

        switch (type)
        {
            case DebuffType.Burn:
            case DebuffType.Posion:
                {
                    while (elapsed < duration)
                    {
                        monster.Damaged(value);

                        if (monster.IsDead)
                        {
                            yield break;
                        }

                        yield return new WaitForSeconds(1f);
                        elapsed += 1f;
                    }

                    break;
                }
            case DebuffType.Slow:
                {
                    float originalMoveSpeed = monster.MonsterStatManager.GetFinalValue(StatType.MoveSpeed);
                    float reducedSpeed      = originalMoveSpeed * (1 - value); // ex) value가 0.3이면 30% 감소

                    Debug.Log(originalMoveSpeed);
                    monster.MonsterStatManager.DecreaseStatValue(StatType.MoveSpeed, StatValueType.Buff, originalMoveSpeed - reducedSpeed);

                    yield return new WaitForSeconds(duration);

                    // yield return 다음 디버프값 복구
                    monster.MonsterStatManager.IncreaseStatValue(StatType.MoveSpeed, StatValueType.Buff, originalMoveSpeed - reducedSpeed);
                    break;
                }
        }

        // 디버프 종료 시, 딕셔너리에서 제거
        activeDebuffs.Remove(type);
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
                    float reducedSpeed      = originalMoveSpeed * (1 - debuffSkill.Duration); // ex) value가 0.3이면 30% 감소

                    Debug.Log($"MoveSpd : {originalMoveSpeed}");
                    monster.MonsterStatManager.DecreaseStatValue(StatType.MoveSpeed, StatValueType.Buff, originalMoveSpeed - reducedSpeed);

                    while (debuffSkill.Duration > 0)
                    {
                        Debug.Log($"{debuffSkill.Duration}초");

                        yield return new WaitForSeconds(1f);
                        debuffSkill.Duration -= 1f;
                    }

                    monster.MonsterStatManager.IncreaseStatValue(StatType.MoveSpeed, StatValueType.Buff, originalMoveSpeed - reducedSpeed);
                    break;
                }
        }

        debuffSkills.Remove(debuffSkill.DebuffType);
    }

    // 몬스터가 죽을때 호출되는 클리어 디버프 함수
    public void ClearAllDebuffs()
    {
        Debug.Log("디버프 삭제");
        foreach (var pair in activeDebuffs)
        {
            StopCoroutine(pair.Value);
        }

        debuffSkills.Clear();
        activeDebuffs.Clear();
    }
}