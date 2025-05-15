using UnityEngine;
using TMPro;

public class UI_StageInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageText, remainMonsterText;

    string 
        format_stageText = "스테이지: <color=#65ECE0>{0} / 10</color>",
        format_remainMonsterText = "남은 몬스터: <color=#FD6C6C>{0}</color>";

    public void ChangeStageText(int stage) => stageText.text = string.Format(format_stageText, stage);
    public void ChangeRemainMonsterText(int monsterLeft) => remainMonsterText.text = string.Format(format_remainMonsterText, monsterLeft);
}
