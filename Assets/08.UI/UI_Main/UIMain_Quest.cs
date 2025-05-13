using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

public class UIMain_Quest : MonoBehaviour
{
    // 퀘스트를 정렬한 패널
    [SerializeField] Transform questPanel;

    // questPanel 로부터 각 항목별로 출력에 필요한 오브젝트들을 가져와 배열로 정리한 것!
    List<Quest_Output_Form> quest_Output_Forms = new List<Quest_Output_Form>();

    private void Awake()
    {
        // savequestdata : 이름, 보상값이 포함되어 있지 않음
        // 그러니 QuestData에 접근할 필요가 있.. 퀘스트 테이블에서 값을 빼와서 쓰면 안되나??

        // 퀘스트 총 수
        int questCount_Total =  TableManager.Instance.GetTable<QuestTable>().DataDic.Count;

        // 실행 전에는 퀘스트 패널에는 자식 오브젝트로 하나의 퀘스트 항목만 존재
        GameObject questForm = questPanel.GetChild(0).gameObject;

        // 실행하고 퀘스트 수에 맞게 퀘스트 항목을 늘려줌
        if (questCount_Total == 0)
        {
            questForm.SetActive(false);
            return;
        }
        // 1번(제일 처음 존재하던 항목에 대해)
        else
        {
            // questObj의 퀘스트를 써줄 오브젝트들을 찾아서 이 묶음을 리스트에 추가
            quest_Output_Forms.Add(new Quest_Output_Form(questForm));
        }
        
        // 퀘스트 항목 제일 처음을 복제하여 추가
        for (int i = 2; i <= questCount_Total; i++)
        {
            // questObj의 퀘스트를 써줄 오브젝트들을 찾아서 이 묶음을 리스트에 추가
            quest_Output_Forms.Add(new Quest_Output_Form(Instantiate(questForm, questPanel)));
        }
    }

    // 시작할 때 모든 퀘스트 항목에 대해 한번씩 호출하여 퀘스트 데이터를 써주게끔
    public void PrintQuest_Init(SaveQuestData saveQuestData)
    {
        // 해당 퀘스트의 ID 넘버
        int questID = saveQuestData.ID;

        // 해당 항목에 써줄 퀘스트 데이터의 불변값
        QuestData questDataTmp = TableManager.Instance.GetTable<QuestTable>().GetDataByID(questID);

        // 퀘스트 이름 및 설명
        quest_Output_Forms[questID].name.text = questDataTmp.QuestName;
        // 보상(현재 아이템이 골드 외에 없기에 보상은 골드만. 보상 아이템 스프라이트 변경은 없음)
        quest_Output_Forms[questID].reward_text.text = questDataTmp.RewardData.RewardGold.ToString();

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
            quest_Output_Forms[questID].progress_text.text = $"{goal} / {goal}";
            // 진행바로 표시
            quest_Output_Forms[questID].progress_bar.fillAmount = 1;
        }
        // 완료되지 않았다면
        else
        {
            // 퀘스트 목표, 진행 값(표시가 목표값을 넘지 않도록 클램프)
            int goal = saveQuestData.Condition.RequiredCount,
                progress_Clamped = Mathf.Clamp(saveQuestData.Condition.CurrentCount, 0, goal);

            // 텍스트로 표시
            quest_Output_Forms[questID].progress_text.text = $"{progress_Clamped} / {goal}";
            // 진행바로 표시
            quest_Output_Forms[questID].progress_bar.fillAmount = progress_Clamped * 100 / goal;
        }
    }
}


// 퀘스트 하나를 표시하는데 필요한 UI 오브젝트들
[Serializable]
public class Quest_Output_Form
{
    public TextMeshProUGUI
        name,
        reward_text,
        progress_text;

    // public Sprite reward_sprite;
    public Image progress_bar;

    public Quest_Output_Form(GameObject questObj)
    {
        // 자식 오브젝트를 가져오면 위에서 아래로 순서대로 가져오기에
        // 하이어라키에서 레벨 패널의 계층도를 수정하면 여기도 바꿔야 함.

        // 글자를 표현하는 텍스트매시프로
        // 0: 퀘스트 이름, 1: 퀘스트 진행도 / 목표, 2: 보상
        TextMeshProUGUI[] texts = questObj.GetComponentsInChildren<TextMeshProUGUI>();
        // 이미지가 너무 많아서 진행도 바는 이름으로 찾기로
        Image progressBar = questObj.transform.Find("ProgressBar").GetComponent<Image>();
        this.name = texts[0];
        this.reward_text = texts[1];
        this.progress_text = texts[2];
        this.progress_bar = progressBar;
    }
}
