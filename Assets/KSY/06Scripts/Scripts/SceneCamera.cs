using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ����
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

    // ���ӻ��¸� playing���� ������ֱ�

    void GotoPlaying() 
    {
        mainCamera.SetActive(true);
        GameManager.instance.m_state = GameManager.GameState.Playing;
        gameObject.SetActive(false);
    }
    void CamMoveUsingScript()
    {
        // 1. Ÿ����ġ�� �̵��ϰ� �ʹ�
        Vector3 target = checkPoint[i].position;
        transform.position = Vector3.Lerp(transform.position, target, mvSpd * Time.deltaTime);
        // 2. Ÿ�ٰ��� �Ÿ��� �������� �ȿ� ������
        // 3. ������ġ�� ����ʹ�
        if (Vector3.Distance(target, transform.position) < 0.1)
        {
            i++;
        }
    }

}
