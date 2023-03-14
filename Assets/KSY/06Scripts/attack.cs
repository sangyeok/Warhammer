using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject bloodFac;
    public Transform hitCube;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameObject blood = Instantiate(bloodFac);
            blood.transform.position = other.transform.position;
            blood.transform.forward = other.transform.forward;
            OnHit();
        }
    }
    void OnHit()
    {
        Collider[] cols = Physics.OverlapBox(hitCube.position, hitCube.localScale * 0.5f);
        print("attack");
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Undead"))
            {
                var enemy = cols[i].GetComponentInChildren<Enemy>();
                //CamShakeManager.Instance.Play();
                if (enemy.enemyHp != 0)
                {
                    enemy.Damage();
                    print("enemy min hp");
                }
            }
            else if (cols[i].CompareTag("Troll"))
            {
                var enemy = cols[i].GetComponentInChildren<Enemy_Mid>();
                //CamShakeManager.Instance.Play();
                if (enemy.enemyHp != 0)
                {
                    enemy.Damage();
                    print("enemy min hp");
                }
            }
            else if (cols[i].CompareTag("Boss"))
            {
                var enemy = cols[i].GetComponentInChildren<Enemy_Boss>();
                //CamShakeManager.Instance.Play();
                if (enemy.enemyHp != 0)
                {
                    enemy.Damage();
                    print("enemy min hp");
                }
            }
        }
    }
}
