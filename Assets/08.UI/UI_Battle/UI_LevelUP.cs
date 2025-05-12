using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UI_LevelUP : MonoBehaviour
{
    [SerializeField] SkillOption[] skillOptions;
    UIManager_Battle uIManager_Battle;

    // 스킬 선택지에 나올 스킬들. 선택지 3개 고정이라기에 크기 고정
    int[] skillOptionIndexs = new int[3];

    private void Awake()
    {
        uIManager_Battle = GetComponentInParent<UIManager_Battle>();
    }

    private void OnEnable()
    {
        SkillOptionInit();
    }

    void SkillOptionInit()
    {
        // 레벨업 할 때 어떤 스킬들이 나오는지? >> 오후에 코드 보고 어떻게 만들지 결정하기
        // 획득 스킬, 레벨은 어디에서 가져올 수 있는지 알아보기 >> 스킬 랜덤 출현 부분이 없으면 만들기
        // 스킬 인덱스로부터 이름, 레벨, 아이콘, 설명을 받아와서 각각 써주기
        for (int i = 0; i < 3; i++)
        {
            //skillOptionIndexs[i] = ;
            //skillOptions[i].name.text = "";
            //skillOptions[i].level.text = $"Lv. {}";
            //skillOptions[i].icon.sprite = ;
            //skillOptions[i].info.text = "";
        }
    }

    public void Button_OptionSelect(int buttonIndex)
    {
        // 스킬 레벨업을 어떻게 하는지 확인 필요
        
        // 버튼을 눌러 선택한 스킬 인덱스는 아래와 같음
        // skillOptionIndexs[buttonIndex]
    }

    // 스킬 선택 버튼에 스킬 정보를 표시할 UI오브젝트들
    [Serializable]
    public class SkillOption
    {
        public TextMeshProUGUI name;
        public TextMeshProUGUI level;
        public Image icon;
        public TextMeshProUGUI info;
    }
}
