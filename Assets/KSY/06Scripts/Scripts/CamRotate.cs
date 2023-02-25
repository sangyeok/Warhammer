using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

// ������� ���콺 �Է¿� ���� ��ü�� ȸ����Ű�� �ʹ�.
// �ʿ�Ӽ� : ȸ���ӵ�, ���콺�Է�, ��ü����, ī�޶����
// 
public class CamRotate : MonoBehaviour
{
    // �ʿ�Ӽ� : ȸ���ӵ�
    public float rotSpeed = 400f;
    float mx;
    float my;
    // Start is called before the first frame update
    void Start()
    {
        mx = transform.eulerAngles.y;
        my = -transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        // ������� ���콺 �Է¿� ���� ��ü�� ȸ����Ű�� �ʹ�.
        // 1. ���콺 �Է�����.
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        // 2. ������ �ʿ�.
        // Vector3 dir = new Vector3(-v, h, 0);
        // 3. ȸ����Ű�� �ʹ�.
        // P = Po + vt
        // transform.eulerAngles += dir * rotSpeed * Time.deltaTime;
        mx += h * rotSpeed * Time.deltaTime;
        my += v * rotSpeed * Time.deltaTime;
        /*if (my < -60) 
        { 
            my = -60; 
        }
        else if (my > 60) 
        { 
            my = 60; 
        }*/
        my = Mathf.Clamp(my, -60, 60);
        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
