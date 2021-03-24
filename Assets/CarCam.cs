using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCam : MonoBehaviour
{
    public Transform CamReference;
    public Vector3 Offset;
    public Transform Car;

    // Update is called once per frame
    void Update()
    {
        transform.position = CamReference.position + Offset;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,CamReference.eulerAngles.y,transform.eulerAngles.z);
    }
}
