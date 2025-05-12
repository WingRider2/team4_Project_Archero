using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_LevelUP : MonoBehaviour
{
    // 유저들에게 정보를 보여주기 위한 UI 오브젝트들
    [SerializeField] SkillOption[] skillOptions;

    // 스킬 선택지에 나온 스킬들의 인덱스. 선택지 3개 고정이라기에 크기 고정
    int[] skillOptionIndexs = new int[3];

    private void OnEnable()
    {
        // 레벨업 고르는 동안은 일시정지
        Time.timeScale = 0;
        SkillOptionInit();
    }

    // 레벨업 패널이 등장할 때, 레벨업 선택지 초기화
    void SkillOptionInit()
    {
        // 랜덤하게 뽑은 3개의 스킬을 가져오게끔
        HashSet<SkillData> skillDatas = new HashSet<SkillData>(SkillManager.Instance.GetSkillToSelect());

        // HashSet은 인덱스가 없으므로 여기서 i를 선언하여 사용
        int i = 0;
        foreach (SkillData skillData in skillDatas)
        {
            // 각 버튼에 어떤 스킬이 들어가 있는지 인덱스 저장
            skillOptionIndexs[i] = skillData.Id;

            // UI 표시 정보 변경
            skillOptions[i].name.text = skillData.Name;
            skillOptions[i].icon.sprite = skillData.SkillIcon;
            // 스킬 설명 없다??? >> 밤에 스킬 데이터 작업해도 되는지 물어보기
            //skillOptions[i].info.text = skillData.;

            // 다음 인덱스로
            i++;
        }
    }

    public void Button_OptionSelect(int buttonIndex)
    {
        // 선택한 버튼과 일치하는 스킬 인덱스를 통해 플레이어 스킬 추가
        SkillManager.Instance.SelectSkill(skillOptionIndexs[buttonIndex]);
        // 일시정지 해제
        Time.timeScale = 1;
        // 스킬 선택이 끝났으니 레벨업 패널 비활성화
        gameObject.SetActive(false);
    }

    // 스킬 선택 버튼에 스킬 정보를 표시할 UI오브젝트들
    [Serializable]
    public class SkillOption
    {
        public TextMeshProUGUI name;
        public Image icon;
        public TextMeshProUGUI info;
    }
}
