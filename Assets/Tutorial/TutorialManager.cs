using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : SceneOnlyManager<TutorialManager>
{
    //Ʃ�丮�� �������� ����
    public GameObject playerPrefab;
    public GameObject monsterPrefab;

    bool canMove = false; // �̵��� �������� Ȯ��
    bool canAttack = false; //������ ��������.

    public string[] tutorialLines;
}
