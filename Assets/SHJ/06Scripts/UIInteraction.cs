using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        lr.SetPosition(0, transform.position);
        int layer = 1 << LayerMask.NameToLayer("UI");
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
        {
            lr.SetPosition(1, hitInfo.point);
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) != 0)
            {
                Button button = hitInfo.transform.GetComponent<Button>();
                button.onClick.Invoke();
            }
        }
        else
        {
            lr.SetPosition(1, transform.position + transform.forward * 1000);

        }
    }
}
