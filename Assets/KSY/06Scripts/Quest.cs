using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject Mark;
    GameObject target;
    public GameObject quad;

    // ����Ʈ
    public GameObject quest00; // ����Ʈ ���� ui ������ quest0 �ѱ�
    public GameObject quest01; // ����Ʈ ���� ui ������ quest1 ����
                               //quest00.SetActive(false);
                               //quest01.SetActive(true);

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

    // ���� ��ũ��Ʈ�� �ִ� ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Potal.GetComponent<ObeliskHit>().isQuest = true;
        }
    }
}
