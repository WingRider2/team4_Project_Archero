using UnityEngine;

public class UIMain_Quest : MonoBehaviour
{
    // 퀘스트를 정렬한 패널
    [SerializeField] Transform questPrefab;

    private void Start()
    {
        // 퀘스트 총 수
        int questCount_Total =  TableManager.Instance.GetTable<QuestTable>().DataDic.Count;
        // 퀘스트 수에 맞게 항목 생성
        for (int i = 1; i <= questCount_Total; i++)
        {
            Instantiate(questPrefab, transform);
        }

    }
}
