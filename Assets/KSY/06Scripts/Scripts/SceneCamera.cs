using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 최종
public class SceneCamera : MonoBehaviour
{
    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Transform[] checkPoint;
    int i;
    public float mvSpd = 1.5f;

    // Update is called once per frame

    // 게임상태를 playing으로 만들어주기

    void GotoPlaying() 
    {
        mainCamera.SetActive(true);
        GameManager.instance.m_state = GameManager.GameState.Playing;
        gameObject.SetActive(false);
    }
    void CamMoveUsingScript()
    {
        // 1. 타겟위치로 이동하고 싶다
        Vector3 target = checkPoint[i].position;
        transform.position = Vector3.Lerp(transform.position, target, mvSpd * Time.deltaTime);
        // 2. 타겟과의 거리가 일정범위 안에 들어오면
        // 3. 다음위치로 가고싶다
        if (Vector3.Distance(target, transform.position) < 0.1)
        {
            i++;
        }
    }

}
