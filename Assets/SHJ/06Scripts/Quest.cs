using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject Mark;
    GameObject target;
    public GameObject potal;

    // ����Ʈ
    public GameObject questUI;
    public GameObject quest00; // ����Ʈ ���� ui ������ quest0 �ѱ�
    public GameObject quest01; // ����Ʈ ���� ui ������ quest1 ����

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

            quest00.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        questUI.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        questUI.SetActive(false);

    }

}
