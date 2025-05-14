using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public SaveData SaveData { get; private set; }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SaveFile()
    {
    }

    public void LoadFile()
    {
    }
}

public class SaveData
{
    public List<SaveQuestData> QuestData = new List<SaveQuestData>();
    public int Gold;

    public SaveData(List<SaveQuestData> questData, int gold)
    {
        QuestData = questData;
        this.Gold = gold;
    }
}