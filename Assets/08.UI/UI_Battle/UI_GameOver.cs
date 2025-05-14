using UnityEngine;
using TMPro;
public class UI_GameOver : MonoBehaviour
{
    // 게임 클리어 여부
    [SerializeField] TextMeshProUGUI ButtonText_Exit, ButtonText_Next;

    private void Awake()
    {
        // 게임 클리어 UI 버튼들: 메인 메뉴로 나가기, 다음 스테이지 2개의 텍스트 사이즈 통일
       UIManager_Battle.Instance.Pause_Buttons_TextSize_Unify(ButtonText_Exit, ButtonText_Next);
    }

    private void OnEnable()
    {
        // 플레이어 사망 여부에 따라 다른 UI 출현
        if(PlayerController.Instance.IsDead)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Button_NextStage()
    {
        Debug.Log("다음 스테이지"); 
    }
}
