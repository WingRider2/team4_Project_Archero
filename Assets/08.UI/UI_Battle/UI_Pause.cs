using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] Transform Panel_Skills;

    // ��ų�� �ε������� ���� ��
    List<int> skills_index;

    UIManager_Battle uIManager_Battle;

    private void Awake()
    {
        uIManager_Battle = GetComponentInParent<UIManager_Battle>();
    }

    // Ȱ��ȭ �� ȹ���� ��ų�鿡 ���� ��ų �������� ����ϰԲ�
    private void OnEnable()
    {
        // !!!!! �÷��̾� ��ų ����Ʈ�� �������� �����ϰԲ� ����� ��
        // SkillIconsInit("�÷��̾� ��ų ����Ʈ");
    }

    void SkillIconsInit(List<int> playerSkills)
    {
        // ������ ��ų ����Ʈ�� ���ٸ� ����
        if (skills_index.SequenceEqual(playerSkills))
            return;

        // ������ ��ų ��
        int previous_skillCount = skills_index.Count;
        // �������� ������ ��ų�鿡 ����
        for (int i = previous_skillCount; i < playerSkills.Count; i++)
        {
            // �ش� ��ġ�� �߰��� ��ų
            int tmpSkill = playerSkills[i];
            // ���� ���� ��ų�� ����Ʈ�� �߰�
            skills_index.Add(tmpSkill);
            // ���� ���� ��ų�� ǥ���� ��Ȱ��ȭ ������ �ڽ� ������Ʈ
            Transform skillIconObject = transform.GetChild(i);
            // ��ų �׵θ�/��ų ���/��ų ������ ���� ������ �Ǿ� ����
            // ��ų ������ Image ������Ʈ�� �����Ͽ� �ش� ��ų ���������� ����
            if (skillIconObject.GetChild(0).GetChild(0).TryGetComponent(out Image iconImage))
                iconImage.sprite = uIManager_Battle.GetSkillIcon(tmpSkill);
            // Ȱ��ȭ�Ͽ� ��ų ������ ǥ��
            skillIconObject.gameObject.SetActive(true);
        }
    }

    // �Ͻ����� �г� ��Ȱ��ȭ
    public void Disable_PausePanel()
    {
        // �ð� ������ �� ���·� ������
        Time.timeScale = 1;
        // �г� ��Ȱ��ȭ
        gameObject.SetActive(false);  
    }
}
