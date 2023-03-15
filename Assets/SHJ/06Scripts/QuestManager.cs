using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//0. (마을) 퀘스트 박스에 다가가면 -> 텔레포트로 이동: ENTER THE DUNGEON

//1.[0] (마을) 침대방을 벗어나오면 -> 퀘스트 얻기: REACH THE TEMP 
//2.[1] (던전) 던전으로 텔레포트하면 -> 이동: CONTINUE ALONG THE WALL
//3.[2] (던전) 던전에서 처음으로 좌회전하면 -> 언데드 무더기 나오는 구간: SURVIVE THE AMBUSH!
//4.[3] (던전) 고블린 석상 앞으로 가면 -> 중간보스: KILL THE TROLL 
//5.[4] (던전) 트롤 죽이고 문 통과하고 나면 -> 보스맵으로 이동: FIND THE CRYSTAL + 문 열기
//6. (던전) 크리스탈에 다가가면 -> 보스 등장: PROTECT THE CRYSTAL FROM THE ENEMY
//7. (던전) 보스 죽으면 -> 보스 죽음: ESCAPE THROUGH THE LIGHT OF OBELISK

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
