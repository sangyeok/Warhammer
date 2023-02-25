using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

// 사용자의 마우스 입력에 따라 물체를 회전시키고 싶다.
// 필요속성 : 회전속도, 마우스입력, 물체방향, 카메라방향
// 
public class CamRotate : MonoBehaviour
{
    // 필요속성 : 회전속도
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
        // 사용자의 마우스 입력에 따라 물체를 회전시키고 싶다.
        // 1. 마우스 입력으로.
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        // 2. 방향이 필요.
        // Vector3 dir = new Vector3(-v, h, 0);
        // 3. 회전시키고 싶다.
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
