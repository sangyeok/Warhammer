using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOw : MonoBehaviour
{
    public Transform upper;
    public Transform lower;
    public LineRenderer upLr;
    public LineRenderer downLr;
    public Transform origin;
    public bool isGrap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        upLr.SetPosition(0, upper.position);
        upLr.SetPosition(1, transform.position);
        downLr.SetPosition(0, lower.position);
        downLr.SetPosition(1, transform.position);

        if (!isGrap)
            transform.position = Vector3.Lerp(transform.position, origin.position, Time.deltaTime * 24);


    }
}
