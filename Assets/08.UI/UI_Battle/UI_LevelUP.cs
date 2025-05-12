using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UI_LevelUP : MonoBehaviour
{
    [SerializeField] SkillOption[] skillOptions;
    UIManager_Battle uIManager_Battle;

    // ��ų �������� ���� ��ų��. ������ 3�� �����̶�⿡ ũ�� ����
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
        // ������ �� �� � ��ų���� ��������? >> ���Ŀ� �ڵ� ���� ��� ������ �����ϱ�
        // ȹ�� ��ų, ������ ��𿡼� ������ �� �ִ��� �˾ƺ��� >> ��ų ���� ���� �κ��� ������ �����
        // ��ų �ε����κ��� �̸�, ����, ������, ������ �޾ƿͼ� ���� ���ֱ�
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
        // ��ų �������� ��� �ϴ��� Ȯ�� �ʿ�
        
        // ��ư�� ���� ������ ��ų �ε����� �Ʒ��� ����
        // skillOptionIndexs[buttonIndex]
    }

    // ��ų ���� ��ư�� ��ų ������ ǥ���� UI������Ʈ��
    [Serializable]
    public class SkillOption
    {
        public TextMeshProUGUI name;
        public TextMeshProUGUI level;
        public Image icon;
        public TextMeshProUGUI info;
    }
}
