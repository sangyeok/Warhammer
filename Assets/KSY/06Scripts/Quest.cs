using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject Mark;
    GameObject target;
    public GameObject quad;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }
    int a = 1;
    void Update()
    {
        if (Mark.transform.localPosition.y < 4.7f)
        {
            a = 1;
        }
        else if (Mark.transform.localPosition.y > 5.3f)
        {
            a = -1;
        }
        Mark.transform.Translate(Vector3.up * 0.5f * Time.deltaTime * a);
        Vector3 dir = transform.position - target.transform.position;
        if (dir.magnitude < 5)
        {
            quad.SetActive(true);
        }
        else
            quad.SetActive(false);
    }

    // 현지 스크립트에 있던 내용
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        quest.SetActive(true);
    //        Potal.GetComponent<ObeliskHit>().isQuest = true;
    //    }
    //}
}
