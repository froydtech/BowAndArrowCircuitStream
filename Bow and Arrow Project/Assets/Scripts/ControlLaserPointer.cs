using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLaserPointer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<LineRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateRay()
    {
        gameObject.GetComponent<LineRenderer>().enabled = true;
    }

    public void DeactivateRay()
    {
        gameObject.GetComponent<LineRenderer>().enabled = false;
    }
}
