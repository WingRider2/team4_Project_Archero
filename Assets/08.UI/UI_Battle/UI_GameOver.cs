using UnityEngine;

public class UI_GameOver : MonoBehaviour
{
    // ���� Ŭ���� ����
    bool isClear;

    private void OnEnable()
    {
        // ���� �� ������ isClear�� Ŭ���� ���� ǥ�ø� �ִ´ٰ� �ϴ� �� ���� �޾ƿ����� �ϱ�
        // isClear

        // ���� Ŭ���� ���ο� ���� �ٸ� UI ����
        transform.GetChild(0).gameObject.SetActive(!isClear);
        transform.GetChild(1).gameObject.SetActive(isClear);
    }

    public void Button_NextStage()
    {
        Debug.Log("���� ��������");
    }
}
