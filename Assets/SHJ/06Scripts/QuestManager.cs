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
    public GameObject quests;
    public GameObject boxes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            quests.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        quests.SetActive(false);
    }
}
