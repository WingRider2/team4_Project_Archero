using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : SceneOnlyManager<TutorialManager>
{
    //튜토리얼 전반적인 관리
    public GameObject playerPrefab;
    public GameObject monsterPrefab;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button NextButton;
    [SerializeField] int nextTextCount = 1;

    bool canMove = false; // 이동
    bool canAttack = false; //공격

    public string[] tutorialLines;
}
