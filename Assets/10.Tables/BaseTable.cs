using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BaseTable<T> : ScriptableObject, ITable where T : class
{
    [SerializeField] protected List<T> dataList = new List<T>();

    public Dictionary<int, T> DataDic { get; private set; } = new Dictionary<int, T>();
    public Type Type { get; private set; }


    public virtual void CreateTable()
    {
        Type = GetType();
    }

    public virtual T GetDataByID(int id)
    {
        if (DataDic.TryGetValue(id, out T value))
            return value;

        Debug.LogError($"ID {id}를 찾을 수 없습니다.");
        return null;
    }
}