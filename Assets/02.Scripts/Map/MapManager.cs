using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Tilemap floorMap;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var monsterTable = TableManager.Instance.GetTable<MonsterTable>();
        var monsterData  = monsterTable.GetDataByID(1);
    }

    void UseSkill()
    {
        //스킬 로직
        
    }
    
}
