using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager_Battle : SceneOnlyManager<UIManager_Battle>
{
    [Header("UI 패널"), SerializeField] Transform Panel_Pause;
    [SerializeField] Transform Panel_Confirm, Panel_LevelUp, Panel_GameOver;

    // 레벨, 골드 : 게임 플레이 중 써주기 위함
    // 계속하기, 나가기 버튼의 텍스트는 글자 크기를 맞춰주기 위해 가져옴
    [Header("텍스트"), SerializeField]
    TextMeshProUGUI gold_text;

    [SerializeField] TextMeshProUGUI level_text, continue_button_text, exit_button_text;

    // 체력바
    [Space(10), SerializeField] RectTransform hpBar;

    // 스테이지 정보 표시를 위해 만든 스크립트
    [Space(10)] public UI_StageInfo stageInfo;

    float targetAspectRatio; // 너비/높이로 게임 화면 비율 계산한 값

    const string MainSceneName = "MainScene";

    protected override void Awake()
    {
        // 다른 스크립트에서도 쉽게 접근 가능하게끔 인스턴스 생성
        base.Awake();

        // 게임 실행시의 해상도 비율을 기억
        if (TryGetComponent(out UnityEngine.UI.CanvasScaler canvasScaler))
        {
            Screen.SetResolution((int)canvasScaler.referenceResolution.x, (int)canvasScaler.referenceResolution.y, false);
            targetAspectRatio = canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y;
        }
        else
            Debug.Log("캔버스 스케일러가 없어요");

        // 일시정지 화면의 버튼 텍스트 크기 통일
        Pause_Buttons_TextSize_Unify(continue_button_text, exit_button_text);
    }

    // RectTransform 자체가 변경되었을 때 호출 >> 빌드 후 창 크기 변경 때 호출
    void OnRectTransformDimensionsChange()
    {
        MaintainAspectRatio();
    }

    /* 사용자가 프로그램 창 크기를 조절할 때 설정한 화면 비율에 맞게 늘어나거나 줄어들게끔
     * 제작 사유 : 빈 부분 보이면 몰입감을 해칠 수 있음
     * 호출 시점 : 창 크기가 바뀔 때마다
     *
     * 상하로 당기는 건 대응이 되나 좌우는 대응이 되지 않아 시도하다 꼬여서 원복... >> 생각보다 깔끔하게 되지는 않네요
     * !!!!! : 당장 급하진 않으니 나중에 수정
     */
    void MaintainAspectRatio()
    {
        // 현재 프로그램창 너비, 높이
        int newWidth  = Screen.width,
            newHeight = Screen.height;

        // 초기 화면비에 맞춰주기
        newWidth = Mathf.RoundToInt(newHeight * targetAspectRatio);

        // 해상도 설정
        Screen.SetResolution(newWidth, newHeight, false);
    }

    // 일시정지 UI 활성화
    public void Enable_Pause() => Panel_Pause.gameObject.SetActive(true);

    // 레벨업 UI 활성화
    public void Enable_LevelUp() => Panel_LevelUp.gameObject.SetActive(true);

    // 게임오버/스테이지 클리어 UI 활성화
    // 플레이어 사망/보스 몬스터 처치 후 호출하기!
    public void Enable_GameOver() => Panel_GameOver.gameObject.SetActive(true);

    // 골드 획득량 표시 변화
    public void SetGoldText(int gold) => gold_text.text = gold.ToString();

    // exp 바 채워진 양 표시 변화
    public void SetExpRatio(float ratio) => hpBar.localScale = new Vector3(ratio, 1, 1);

    // 일시 정지 중 나가기 버튼을 눌렀을 때 확인 창 팝업 + 확인 창 끄기
    public void ChangeActive_Confirm() => Panel_Confirm.gameObject.SetActive(!Panel_Confirm.gameObject.activeSelf);

    // 메인 씬으로 나가기
    public void SceneChange_Main() => SceneManager.LoadScene(MainSceneName);

    // 레벨 표시 변경
    public void SetLevelText(int nextLevel) => level_text.text = $"Lv. {nextLevel}";

    /*
       일시정지 UI의 전투 나가기, 계속하기 버튼의 안내 텍스트 사이즈 통일
       테스트 결과 게임 실행 때 1번만 맞춰주면 이후에 창 크기를 줄이거나 늘려도 텍스트 사이즈가 동일함을 확인
       제작 사유 : 글자 크기 차이가 나면 없어보임
    */
    public void Pause_Buttons_TextSize_Unify(TextMeshProUGUI text1, TextMeshProUGUI text2)
    {
        // 폰트 자동 사이즈 옵션을 둘 다 켜주기
        text1.enableAutoSizing = text2.enableAutoSizing = true;

        // 폰트 자동 사이즈 옵션 적용 시 나가기, 계속하기 버튼의 텍스트 사이즈
        float
            continue_text_size = text1.fontSize,
            exit_text_size     = text2.fontSize;

        // 텍스트 사이즈가 작은 쪽으로 통일 
        if (continue_text_size > exit_text_size)
        {
            // 자동 텍스트 사이즈 옵션 해제하고, 텍스트 사이즈 통일
            text1.enableAutoSizing = false;
            text1.fontSize = exit_text_size;
        }
        else
        {
            text2.enableAutoSizing = false;
            text2.fontSize = continue_text_size;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}