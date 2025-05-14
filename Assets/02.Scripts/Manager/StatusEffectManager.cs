using System.Collections;
using System.Collections.Generic;
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

    private IEnumerator HandleDebuff(DebuffType type, float damage, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            monster.Damaged(damage);
            if (monster.IsDead)
            {
                // monster.SendMessage("Dead", SendMessageOptions.DontRequireReceiver);
                activeDebuffs.Clear();
                yield break;
            }

            yield return new WaitForSeconds(1f);
            elapsed += 1f;
        }

        // 디버프 종료 시, 딕셔너리에서 제거
        activeDebuffs.Remove(type);
    }

    // 몬스터가 죽을때 호출되는 클리어 디버프 함수
    public void ClearAllDebuffs()
    {
        Debug.Log("디버프 삭제");
        foreach (var pair in activeDebuffs)
        {
            StopCoroutine(pair.Value);
        }

        activeDebuffs.Clear();
    }
}