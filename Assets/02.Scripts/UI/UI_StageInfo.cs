using UnityEngine;
using TMPro;

public class UI_StageInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageText, remainMonsterText;

    string 
        format_stageText = "스테이지: <color=#65ECE0>{0} / {1}</color>",
        format_remainMonsterText = "남은 몬스터: <color=#FD6C6C>{0}</color>";

    int maxStage;

    private void Start()
    {
        maxStage = TableManager.Instance.GetTable<StageTable>().GetDataByID(GameManager.Instance.SelectedChapter).StageDatas.Count;
    }

    public void ChangeStageText(int currentStage) => stageText.text = string.Format(format_stageText, currentStage, maxStage);
    public void ChangeRemainMonsterText(int monsterLeft) => remainMonsterText.text = string.Format(format_remainMonsterText, monsterLeft);
}
