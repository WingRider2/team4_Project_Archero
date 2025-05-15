using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UIManager_Main : MonoBehaviour
{
    [SerializeField]
    Transform
        panel_StageSelect; // 스테이지 선택 버튼들이 있는 패널
        // panel_Setting; // 설정 메뉴가 있는 패널... 이었으나 설정에서 이동밖에 없는 키 설정. 그것도 보편적인 이동 방식인 wasd, 상하좌우 키를 바꿀 필요가 있냐는 물음에 쓰지 않기로 함

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