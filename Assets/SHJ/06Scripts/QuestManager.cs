using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//0. (����) ����Ʈ �ڽ��� �ٰ����� -> �ڷ���Ʈ�� �̵�: ENTER THE DUNGEON

//1.[0] (����) ħ����� ������� -> ����Ʈ ���: REACH THE TEMP 
//2.[1] (����) �������� �ڷ���Ʈ�ϸ� -> �̵�: CONTINUE ALONG THE WALL
//3.[2] (����) �������� ó������ ��ȸ���ϸ� -> �𵥵� ������ ������ ����: SURVIVE THE AMBUSH!
//4.[3] (����) ��� ���� ������ ���� -> �߰�����: KILL THE TROLL 
//5.[4] (����) Ʈ�� ���̰� �� ����ϰ� ���� -> ���������� �̵�: FIND THE CRYSTAL + �� ����
//6. (����) ũ����Ż�� �ٰ����� -> ���� ����: PROTECT THE CRYSTAL FROM THE ENEMY
//7. (����) ���� ������ -> ���� ����: ESCAPE THROUGH THE LIGHT OF OBELISK

public class QuestManager : MonoBehaviour
{
    public GameObject[] quests;
    public GameObject[] boxes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(gameObject == boxes[0])
            {
                quests[0].SetActive(true);
            } 
            else if(gameObject == boxes[1])
            {
                quests[1].SetActive(true);
            } 
            else if(gameObject == boxes[2])
            {
                quests[1].SetActive(false);
                quests[2].SetActive(true);
            }
            else if(gameObject == boxes[3])
            {
                quests[2].SetActive(false);
                quests[3].SetActive(true);
            }
            else if(gameObject == boxes[4])
            {
                quests[3].SetActive(false);
                quests[4].SetActive(true);
            }
        }
    }




    public GameObject dungeonQuestBefore;
    public GameObject dungeonQuest;
}
