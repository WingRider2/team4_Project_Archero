using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lower : MonoBehaviour
{
    [SerializeField] GameObject UI_Middle;
    // 메인씬의 화면 하단 UI의 버튼에 1:1로 대응하는 패널을 모아놓은 변수
    [SerializeField] GameObject[] panelForButton;

    public void Button_LowerUI(int index)
    {
        int previous = -1;

        // 하단 버튼에 대응하는 UI들을 모두 비활성화 및 열려있던 UI의 인덱스 기억
        for (int i = 0; i < panelForButton.Length; i++)
        {
            if (panelForButton[i].activeSelf)
                previous = i;
            panelForButton[i].SetActive(false);
        }

        // 이전에 열려있던 UI와 동일한 버튼을 눌렀다면, 해당 UI는 닫고, UI_Middle 활성화
        if (previous == index)
            UI_Middle.SetActive(true);
        // 이전에 열려있던 UI와 다른 버튼을 눌렀다면, 해당 UI 활성화
        else
        {
            panelForButton[index].SetActive(true);
            UI_Middle.SetActive(false);
        }
    }
}
