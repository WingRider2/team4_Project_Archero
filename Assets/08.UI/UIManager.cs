using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform Panel_Pause;
    [SerializeField] TextMeshProUGUI continue_button_text, 
                                     exit_button_text;

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
        Pause_Buttons_TextSize_Unify(); // �Ͻ����� ȭ���� ��ư �ؽ�Ʈ ũ�� ����
    }

    // RectTransform ��ü�� ����Ǿ��� �� ȣ�� >> ���� �� â ũ�� ���� ���� ȣ��� ������ ����
    void OnRectTransformDimensionsChange()
    {
        //Debug.Log("RectTransform ũ�� �����!");
        MaintainAspectRatio();
    }

    /* ����ڰ� ���α׷� â ũ�⸦ ������ �� ������ ȭ�� ������ �°� �þ�ų� �پ��Բ�
     * ���� ���� : �� �κ� ���̸� ���԰��� ��ĥ �� ����
     * ȣ�� ���� : â ũ�Ⱑ �ٲ� ������
     * 
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

    // �������� ������ ��ư
    public void Click_Button_Exit()
    {
        // !!!!! �������� ���� �� ��ȯ �Ŵ����� ���� ���̶�� �װ� ������ ����. ���� ���ٰ� �ϸ� ���⼭ �� ��ȯ�غ���
        // ���� ��ư�� ������ �ص� ����
        Debug.Log("������!");
    }

    /*
       �Ͻ����� �г� Ȱ��ȭ/��Ȱ��ȭ ��ȯ
       ����ϴ� ��
       1) Panel_UpperUI_Battle >> Button_Pause
       2) Panel_Pause >> Button_Continue
    */
    public void Change_PausePanel_Activity()
    {
        // ���� ���¿��� not�� �ٿ� ���� ���·� ��ȭ (Ȱ��ȭ ���¿��� �ش� �޼��带 �۵��Ͽ��ٸ� ��Ȱ��ȭ)
        bool isActive = !Panel_Pause.gameObject.activeSelf;

        // �г� Ȱ��ȭ/��Ȱ��ȭ
        Panel_Pause.gameObject.SetActive(isActive);

        // �Ͻ�����/����
        if (isActive)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    /*
       �Ͻ����� UI�� ���� ������, ����ϱ� ��ư�� �ȳ� �ؽ�Ʈ ������ ����
       �׽�Ʈ ��� ���� ���� �� 1���� �����ָ� ���Ŀ� â ũ�⸦ ���̰ų� �÷��� �ؽ�Ʈ ����� �������� Ȯ��
       ���� ���� : ���� ũ�� ���̰� ���� �����
    */
    public void Pause_Buttons_TextSize_Unify()
    {
        // ��Ʈ �ڵ� ������ �ɼ��� �� �� ���ֱ�
        continue_button_text.enableAutoSizing = exit_button_text.enableAutoSizing = true;

        // ��Ʈ �ڵ� ������ �ɼ� ���� �� ������, ����ϱ� ��ư�� �ؽ�Ʈ ������
        float
            continue_text_size = continue_button_text.fontSize,
            exit_text_size = exit_button_text.fontSize;

        // �ؽ�Ʈ ����� ���� ������ ���� 
        if (continue_text_size > exit_text_size)
        {
            // �ڵ� �ؽ�Ʈ ������ �ɼ� �����ϰ�, �ؽ�Ʈ ������ ����
            continue_button_text.enableAutoSizing = false;
            continue_button_text.fontSize = exit_text_size;
        }
        else
        {
            exit_button_text.enableAutoSizing = false;
            exit_button_text.fontSize = continue_text_size;
        }
    }
}
