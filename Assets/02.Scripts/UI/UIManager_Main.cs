using UnityEngine;
using UnityEditor;

public class UIManager_Main : MonoBehaviour
{
    [SerializeField]
    Transform
        panel_StageSelect; // 스테이지 선택 버튼들이 있는 패널
       // panel_Setting;     // 설정 메뉴가 있는 패널

    // target으로 설정한 오브젝트 활성화/비활성화 전환
    // 패널을 불러오는 버튼들에 할당
    public void ChangeActive(GameObject target) => target.SetActive(!target.activeSelf);

    // 나가기 버튼에 할당. 게임 종료 기능
    public void GameQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 에디터 플레이 모드 종료
#else
        Application.Quit();
#endif
    }
}