using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFan : MonoBehaviour
{
    public float Length;

    public Transform Body;
    public Rigidbody Rb;
    public float Force;
    RaycastHit hit;
    float lastRatio = 0f;

    
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -Body.transform.forward);

        if (Physics.Raycast(ray, out hit, Length))
        {
             Debug.Log(transform.localPosition);
            float ratio = (Length / hit.distance);

            /* if (Mathf.Abs(ratio - lastRatio) > Damping)
             {
                 Rb.AddForceAtPosition(Force * Time.fixedDeltaTime * ratio * Rb.mass * Body.transform.up, transform.position);
             }*/

            Rb.AddForceAtPosition(Force * Time.fixedDeltaTime * ratio * Rb.mass * Body.transform.up, transform.position);

            lastRatio = ratio;
        }
 
    }

    private void OnDrawGizmos()
    {

        if (hit.point != Vector3.zero)
        {
            Gizmos.DrawSphere(Rb.ClosestPointOnBounds(transform.position), 0.1f);
            Gizmos.DrawLine(transform.position, hit.point);
        }
    }
}
