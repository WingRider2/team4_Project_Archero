using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string path = "saveData.json";
    public SaveData SaveData { get; private set; }

    void Start()
    {
    }

    void Update()
    {
    }

    public void SaveFile()
    {
        if (SaveData == null)
        {
            SaveData = new SaveData(QuestManager.Instance.QusetList, AccountManager.Instance.Gold);
        }

        var sJson = JsonConvert.SerializeObject(SaveData, Formatting.Indented);
        File.WriteAllText(path, sJson);
    }

    public void LoadFile()
    {
        try
        {
            var encrypted = File.ReadAllText(path);
            var data      = JsonConvert.DeserializeObject<SaveData>(encrypted);
            if (data != null)
            {
                SaveData = new SaveData(data);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("⚠ 저장 파일이 손상되었거나 로드에 실패했습니다.");
            File.Delete(path);
        }
    }
}

public class SaveData
{
    public List<SaveQuestData> QuestData = new List<SaveQuestData>();
    public int Gold;
    public int ClearChapter;

    public SaveData(List<SaveQuestData> questData, int gold)
    {
        QuestData = questData;
        this.Gold = gold;
        ClearChapter = GameManager.Instance.BestChapter;
    }

    public SaveData(SaveData data)
    {
        QuestData = data.QuestData;
        Gold = data.Gold;
    }
}