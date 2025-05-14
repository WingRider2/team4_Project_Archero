using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine.Serialization;

public class SaveManager : Singleton<SaveManager>
{
    private string path;
    public SaveData SaveData { get; private set; } = new SaveData();

    void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadFile();
    }

    public void SaveFile()
    {
        if (SaveData == null)
        {
            SaveData = new SaveData(QuestManager.Instance.QuestList, AccountManager.Instance.Gold);
        }
        else
        {
            SaveData.QuestData = QuestManager.Instance.QuestList;
            SaveData.Gold = AccountManager.Instance.Gold;
            SaveData.BestChapter = GameManager.Instance.BestChapter;
        }

        var sJson = JsonConvert.SerializeObject(SaveData, Formatting.Indented);
        File.WriteAllText(path, sJson);
    }

    public void LoadFile()
    {
        try
        {
            if (!File.Exists(path))
            {
                SaveData = new SaveData(new List<SaveQuestData>(), 0);
                return;
            }

            string encrypted = File.ReadAllText(path);
            SaveData = JsonConvert.DeserializeObject<SaveData>(encrypted);
        }
        catch (Exception e)
        {
            print("⚠ 저장 파일이 손상되었거나 로드에 실패했습니다.");
            File.Delete(path);
        }
    }

    private void OnApplicationQuit()
    {
        SaveFile();
    }
}

[Serializable]
public class SaveData
{
    public List<SaveQuestData> QuestData = new List<SaveQuestData>();
    public int Gold;

    public int BestChapter;

    public SaveData(List<SaveQuestData> questData, int gold)
    {
        QuestData = questData;
        this.Gold = gold;
        BestChapter = GameManager.Instance.BestChapter;
    }

    public SaveData(SaveData data)
    {
        QuestData = data.QuestData;
        Gold = data.Gold;
        BestChapter = data.BestChapter;
    }

    public SaveData()
    {
    }
}