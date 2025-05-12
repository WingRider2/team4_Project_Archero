using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] Transform Panel_Skills;

    // 스킬의 인덱스들을 모은 것
    List<int> skills_index;

    // 활성화 때 획득한 스킬들에 맞춰 스킬 아이콘을 출력하게끔
    private void OnEnable()
    {
        // 시간 비율을 0으로 >> 게임 일시정지
        Time.timeScale = 0;
        // 플레이어 스킬 리스트만 가져오면 동작하게끔 만들어 둠
        SkillIconsInit(SkillManager.Instance.SelectedSKills);
    }

    void SkillIconsInit(List<ISkill> playerSkills)
    {
        // 기존의 스킬 수
        int previous_skillCount = skills_index.Count;
        // 이전보다 많아진 스킬들에 대해
        for (int i = previous_skillCount; i < playerSkills.Count; i++)
        {
            // 해당 위치에 추가할 스킬
            int tmpSkill = playerSkills[i].Id;
            // 새로 얻은 스킬을 리스트에 추가
            skills_index.Add(tmpSkill);
            // 새로 얻은 스킬을 표시할 비활성화 상태의 자식 오브젝트
            Transform skillIconObject = transform.GetChild(i);
            // 스킬 테두리/스킬 배경/스킬 아이콘 계층 순서로 되어 있음
            // 스킬 아이콘 Image 컴포넌트에 접근
            if (skillIconObject.GetChild(0).GetChild(0).TryGetComponent(out Image iconImage))
            {
                // 해당 스킬에 알맞는 아이콘 설정
                iconImage.sprite = TableManager.Instance.GetTable<SkillTable>().GetDataByID(tmpSkill).SkillIcon;
                // 활성화하여 스킬 아이콘을 화면에 표시
                skillIconObject.gameObject.SetActive(true);
            }
        }
    }

    // 일시정지 패널 비활성화
    public void Disable_PausePanel()
    {
        // 시간 비율을 원 상태로 돌리고
        Time.timeScale = 1;
        // 패널 비활성화
        gameObject.SetActive(false);  
    }
}
