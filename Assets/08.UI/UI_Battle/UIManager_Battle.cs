using UnityEngine;
using TMPro;

public class UIManager_Battle : MonoBehaviour
{
    [SerializeField] Transform Panel_Pause;
    
    // ����, ��� : ���� �÷��� �� ���ֱ� ����
    // ����ϱ�, ������ ��ư�� �ؽ�Ʈ�� ���� ũ�⸦ �����ֱ� ���� ������
    [SerializeField] 
    TextMeshProUGUI 
        gold_text,
        level_text,
        continue_button_text, 
        exit_button_text;

    // ü�¹�
    [SerializeField] RectTransform hpBar;

    // ��ų ������. ��ų�� ������ ���� �ʿ䰡 ���� !!!!!
    [SerializeField] Sprite[] skill_icons;
    public Sprite GetSkillIcon(int index) => skill_icons[index];


    float targetAspectRatio; // �ʺ�/���̷� ���� ȭ�� ���� ����� ��

    void Awake()
    {
        // ���� ������� �ػ� ������ ���
        if (TryGetComponent(out UnityEngine.UI.CanvasScaler canvasScaler))
        {
            Screen.SetResolution((int)canvasScaler.referenceResolution.x, (int)canvasScaler.referenceResolution.y, false);
            targetAspectRatio = canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y;
        }
        else
            Debug.Log("ĵ���� �����Ϸ��� �����");

        // �Ͻ����� ȭ���� ��ư �ؽ�Ʈ ũ�� ����
        Pause_Buttons_TextSize_Unify(continue_button_text, exit_button_text); 
    }

    // RectTransform ��ü�� ����Ǿ��� �� ȣ�� >> ���� �� â ũ�� ���� �� ȣ��
    void OnRectTransformDimensionsChange()
    {
        MaintainAspectRatio();
    }

    /* ����ڰ� ���α׷� â ũ�⸦ ������ �� ������ ȭ�� ������ �°� �þ�ų� �پ��Բ�
     * ���� ���� : �� �κ� ���̸� ���԰��� ��ĥ �� ����
     * ȣ�� ���� : â ũ�Ⱑ �ٲ� ������
     * 
     * ���Ϸ� ���� �� ������ �ǳ� �¿�� ������ ���� �ʾ� �õ��ϴ� ������ ����... >> �������� ����ϰ� ������ �ʳ׿�
     * !!!!! : ���� ������ ������ ���߿� ����
     */
    void MaintainAspectRatio()
    {
        // ���� ���α׷�â �ʺ�, ����
        int newWidth = Screen.width,
            newHeight = Screen.height;

        // �ʱ� ȭ��� �����ֱ�
        newWidth = Mathf.RoundToInt(newHeight * targetAspectRatio);

        // �ػ� ����
        Screen.SetResolution(newWidth, newHeight, false);
    }

    // �Ͻ����� �г� Ȱ��ȭ
    public void Eable_PausePanel()
    {
        // �ð� ������ 0���� >> ���� �Ͻ�����
        Time.timeScale = 0;
        // �Ͻ����� �г� Ȱ��ȭ
        Panel_Pause.gameObject.SetActive(false);
    }

    // ��� ȹ�淮 ǥ�� ��ȭ
    public void SetGoldText(int gold)
    {
        gold_text.text = gold.ToString();
    }

    // exp �� ä���� �� ǥ�� ��ȭ
    public void SetExpRatio(float ratio)
    {
        hpBar.localScale = new Vector3(ratio, 1, 1);
    }

    // �������� ������ ��ư
    public void Button_Exit()
    {
        // !!!!! �������� ���� �� ��ȯ �Ŵ����� ���� ���̶�� �װ� ������ ����. ���� ���ٰ� �ϸ� ���⼭ �� ��ȯ�غ���
        // ���� ��ư�� ������ �ص� ����
        Debug.Log("������!");
    }

    // ���� ǥ�� ����
    public void SetLevelText(int nextLevel)
    {
        level_text.text = $"Lv. {nextLevel}";
    }

    /*
       �Ͻ����� UI�� ���� ������, ����ϱ� ��ư�� �ȳ� �ؽ�Ʈ ������ ����
       �׽�Ʈ ��� ���� ���� �� 1���� �����ָ� ���Ŀ� â ũ�⸦ ���̰ų� �÷��� �ؽ�Ʈ ����� �������� Ȯ��
       ���� ���� : ���� ũ�� ���̰� ���� �����
    */
    public void Pause_Buttons_TextSize_Unify(TextMeshProUGUI text1, TextMeshProUGUI text2)
    {
        // ��Ʈ �ڵ� ������ �ɼ��� �� �� ���ֱ�
        text1.enableAutoSizing = text2.enableAutoSizing = true;

        // ��Ʈ �ڵ� ������ �ɼ� ���� �� ������, ����ϱ� ��ư�� �ؽ�Ʈ ������
        float
            continue_text_size = text1.fontSize,
            exit_text_size = text2.fontSize;

        // �ؽ�Ʈ ����� ���� ������ ���� 
        if (continue_text_size > exit_text_size)
        {
            // �ڵ� �ؽ�Ʈ ������ �ɼ� �����ϰ�, �ؽ�Ʈ ������ ����
            text1.enableAutoSizing = false;
            text1.fontSize = exit_text_size;
        }
        else
        {
            text2.enableAutoSizing = false;
            text2.fontSize = continue_text_size;
        }
    }
}
