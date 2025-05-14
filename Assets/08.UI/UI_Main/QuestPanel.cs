using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI
    name_text,
    reward_text,
    progress_text;

    // public Sprite reward_sprite; // 골드 외 다른 아이템이 없기에 스프라이트는 바꾸지 않는 것으로..
    [SerializeField] Image progress_bar;

    [SerializeField] Button GetReward_Button;

    private void Start()
    {
        // 해당 퀘스트 번호에 해당하는 퀘스트 정보를 써주기!
        // transform.GetSiblingIndex() : 1번부터 시작. 마침 퀘스트도 1번부터 시작하기에 바로 사용 가능
        PrintQuest_Init(QuestManager.Instance.QusetList[transform.GetSiblingIndex()]);
    }

    // 시작할 때 모든 퀘스트 항목에 대해 한번씩 호출하여 퀘스트 데이터를 써주게끔
    void PrintQuest_Init(SaveQuestData saveQuestData)
    {
        // 해당 퀘스트의 ID 넘버
        int questID = saveQuestData.ID;

        // 해당 항목에 써줄 퀘스트 데이터의 불변값
        QuestData questDataTmp = TableManager.Instance.GetTable<QuestTable>().GetDataByID(questID);

        // 퀘스트 이름 및 설명
        name_text.text = string.Format(questDataTmp.QuestName, saveQuestData.Condition.RequiredCount);
        // 보상(현재 아이템이 골드 외에 없기에 보상은 골드만. 보상 아이템 스프라이트 변경은 없음)
        reward_text.text = questDataTmp.RewardData.RewardGold.ToString();

        // 퀘스트 진행도 표시 변경
        PrintQuest_Progress(saveQuestData);
    }

    // 진행 중에 퀘스트 진행도가 경신되었을 때 호출하여 바뀌는 항목만 표시할 수 있게끔
    public void PrintQuest_Progress(SaveQuestData saveQuestData)
    {
        // 해당 퀘스트의 ID 넘버
        int questID = saveQuestData.ID;

        // 완료한 퀘스트라면
        if (saveQuestData.IsComplete)
        {
            // 퀘스트 목표
            int goal = saveQuestData.Condition.RequiredCount;
            // 텍스트로 표시
            progress_text.text = $"{goal} / {goal}";
            // 진행바로 표시
            progress_bar.fillAmount = 1;

            // 이미 완료한 퀘스트라면, 보상 받기 버튼 숨기기
            // 를 하려는데 ClearQuestList는 현재 사용중이지 않는 듯
            //if (QuestManager.Instance.ClearQuestList.Contains(questID))
            //    GetReward_Button.gameObject.SetActive(false);

            // 임시로 보상 받기 버튼 활성화만
            GetReward_Button.gameObject.SetActive(true);
        }
        // 완료되지 않았다면
        else
        {
            // 퀘스트 목표, 진행 값(표시가 목표값을 넘지 않도록 클램프)
            int goal = saveQuestData.Condition.RequiredCount,
                progress_Clamped = Mathf.Clamp(saveQuestData.Condition.CurrentCount, 0, goal);

            // 텍스트로 표시
            progress_text.text = $"{progress_Clamped} / {goal}";
            // 진행바로 표시
            progress_bar.fillAmount = progress_Clamped * 100 / goal;
            // 보상 받기 버튼 비활성화
            GetReward_Button.gameObject.SetActive(false);
        }
    }

    // 퀘스트 버튼을 눌렀을 때, 보상 받기
    public void GetQuestReward()
    {
        QuestData tmpData = TableManager.Instance.GetTable<QuestTable>().GetDataByID(transform.GetSiblingIndex());

        // 아래의 보상 골드를 보유 골드에 더해주기
        // tmpData.RewardData.RewardGold;
    }
}
