using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform Panel_Pause;
    [SerializeField] TextMeshProUGUI continue_button_text, 
                                     exit_button_text;

    float targetAspectRatio; // 너비/높이로 게임 화면 비율 계산한 값

    void Awake()
    {
        // 게임 실행시의 해상도 비율을 기억
        if (TryGetComponent(out UnityEngine.UI.CanvasScaler canvasScaler))
        {
            Screen.SetResolution((int)canvasScaler.referenceResolution.x, (int)canvasScaler.referenceResolution.y, false);
            targetAspectRatio = canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y;
        }
        else
            Debug.Log("캔버스 스케일러가 없어요");
        Pause_Buttons_TextSize_Unify(); // 일시정지 화면의 버튼 텍스트 크기 통일
    }

    // RectTransform 자체가 변경되었을 때 호출 >> 빌드 후 창 크기 변경 때도 호출될 것으로 예상
    void OnRectTransformDimensionsChange()
    {
        //Debug.Log("RectTransform 크기 변경됨!");
        MaintainAspectRatio();
    }

    /* 사용자가 프로그램 창 크기를 조절할 때 설정한 화면 비율에 맞게 늘어나거나 줄어들게끔
     * 제작 사유 : 빈 부분 보이면 몰입감을 해칠 수 있음
     * 호출 시점 : 창 크기가 바뀔 때마다
     * 
     * !!!!! : 당장 급하진 않으니 나중에 수정
     */
    void MaintainAspectRatio()
    {
        // 현재 프로그램창 너비, 높이
        int newWidth = Screen.width,
            newHeight = Screen.height;

        // 초기 화면비에 맞춰주기
        newWidth = Mathf.RoundToInt(newHeight * targetAspectRatio);

        // 해상도 설정
        Screen.SetResolution(newWidth, newHeight, false);
    }

    // 전투에서 나가기 버튼
    public void Click_Button_Exit()
    {
        // !!!!! 공용으로 쓰는 씬 전환 매니저를 만들 것이라면 그걸 가져다 쓰기. 따로 없다고 하면 여기서 씬 전환해보자
        // 현재 버튼에 연결은 해둔 상태
        Debug.Log("나가기!");
    }

    /*
       일시정지 패널 활성화/비활성화 전환
       사용하는 곳
       1) Panel_UpperUI_Battle >> Button_Pause
       2) Panel_Pause >> Button_Continue
    */
    public void Change_PausePanel_Activity()
    {
        // 현재 상태에서 not을 붙여 다음 상태로 변화 (활성화 상태에서 해당 메서드를 작동하였다면 비활성화)
        bool isActive = !Panel_Pause.gameObject.activeSelf;

        // 패널 활성화/비활성화
        Panel_Pause.gameObject.SetActive(isActive);

        // 일시정지/해제
        if (isActive)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    /*
       일시정지 UI의 전투 나가기, 계속하기 버튼의 안내 텍스트 사이즈 통일
       테스트 결과 게임 실행 때 1번만 맞춰주면 이후에 창 크기를 줄이거나 늘려도 텍스트 사이즈가 동일함을 확인
       제작 사유 : 글자 크기 차이가 나면 없어보임
    */
    public void Pause_Buttons_TextSize_Unify()
    {
        // 폰트 자동 사이즈 옵션을 둘 다 켜주기
        continue_button_text.enableAutoSizing = exit_button_text.enableAutoSizing = true;

        // 폰트 자동 사이즈 옵션 적용 시 나가기, 계속하기 버튼의 텍스트 사이즈
        float
            continue_text_size = continue_button_text.fontSize,
            exit_text_size = exit_button_text.fontSize;

        // 텍스트 사이즈가 작은 쪽으로 통일 
        if (continue_text_size > exit_text_size)
        {
            // 자동 텍스트 사이즈 옵션 해제하고, 텍스트 사이즈 통일
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
