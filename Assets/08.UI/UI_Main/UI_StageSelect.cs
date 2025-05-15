using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_StageSelect : MonoBehaviour
{
    const string PlaySceneName = "SampleScene";

    private void Start()
    {
        Button[] chapterButtons = new Button[] { };
        // 최대 챕터의 수에 맞게 버튼이 나오게끔
        chapterButtons = GetComponentsInChildren<Button>(true);
        int maxChapter = TableManager.Instance.GetTable<StageTable>().DataDic.Count;
        // 0번은 뒤로 가기 버튼. 그리고 퀘스트 인덱스는 1부터 시작
        for (int i = 1; i< chapterButtons.Length; i++)
        {
            // 최대 챕터 수를 넘는 버튼들은 비활성화
            if (i > maxChapter)
                chapterButtons[i].gameObject.SetActive(false);
            // 해당 스테이지 버튼에 몇번 스테이지를 지정하여 이동하는지 지정
            else 
            {
                if (i > GameManager.Instance.BestChapter)
                {
                    // 최고 스테이지 다음 스테이지 이후의 스테이지에 접근하지 못하도록 비활성화
                    chapterButtons[i].enabled = false;
                    
                    if(chapterButtons[i].TryGetComponent(out Image tmpButtonImage))             // 해당 버튼의 이미지를 받아왔다면,
                        if (ColorUtility.TryParseHtmlString("#787878", out Color disableColor)) // 짙은 회색을 비활성화 컬러로 곱해주기
                            tmpButtonImage.color *= disableColor;

                }
                int tmpIndex = i;
                chapterButtons[i].onClick.AddListener(() => Button_StageSelect(tmpIndex));
            }
        }
    }

    public void Button_StageSelect(int chapter)
    {
        // 해당 버튼이 가리키는 스테이지로 전환
        GameManager.Instance.SelectedChapter = chapter;
        SceneManager.LoadScene(PlaySceneName);
    }
}
