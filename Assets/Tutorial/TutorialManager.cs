using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : SceneOnlyManager<TutorialManager>
{
    //튜토리얼 전반적인 관리
    public GameObject playerPrefab;
    public GameObject monsterPrefab;

    bool canMove = false; // 이동이 가능한지 확인
    bool canAttack = false; //공격이 가능한지.

    public string[] tutorialLines;
}
