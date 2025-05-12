using UnityEngine;

public class UI_GameOver : MonoBehaviour
{
    // 게임 클리어 여부
    bool isClear;

    private void OnEnable()
    {
        // 몬스터 다 잡으면 isClear로 클리어 상태 표시를 넣는다고 하니 그 값을 받아오도록 하기
        // isClear

        // 게임 클리어 여부에 따라 다른 UI 출현
        transform.GetChild(0).gameObject.SetActive(!isClear);
        transform.GetChild(1).gameObject.SetActive(isClear);
    }

    public void Button_NextStage()
    {
        Debug.Log("다음 스테이지");
    }
}
