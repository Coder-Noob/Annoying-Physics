using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{

   
    [SerializeField]
    private Rigidbody car;

    [SerializeField]
    private Transform tireMesh;
    
    [SerializeField]
    private Vector3 TireMeshOffest = new Vector3(0, -0.5f, 0);

    [SerializeField]
    private LayerMask WhatIsGround;

    public float SuspensionLength;

    public float Damping = 0.2f;

    public float SuspensionStrength = 1f;

    public float MotorForce = 10f;

    public bool ActivateMotor = true;

    public bool DoStreering = true;

    private Transform body;

    RaycastHit hit;

    bool hadHitGround;

    Vector2 input;

    public float Speed;
    public Vector3 ForwardAxis = Vector3.forward;


    private void Awake()
    {
        ForwardAxis.Normalize();
        body = car.transform;
    }

    private void FixedUpdate()
    {
        applySuspension();

        takeInput();
        if(ActivateMotor)
            applyMotorForce();

        if(hadHitGround)
            tireMesh.localPosition = new Vector3(tireMesh.localPosition.x, -hit.distance,tireMesh.localPosition.z) + TireMeshOffest;
        else
            tireMesh.localPosition = new Vector3(tireMesh.localPosition.x, - SuspensionLength, tireMesh.localPosition.z) + TireMeshOffest;
    }

    private void applyMotorForce()
    {
        var force = input.y * Time.deltaTime * Speed * transform.TransformDirection(ForwardAxis);
        car.AddForceAtPosition(force, transform.position);
    }

    private void takeInput()
    {
        input.x = Input.GetAxisRaw(Constants.HORIZONTAL_INPUT_AXIS);
        input.y = Input.GetAxisRaw(Constants.VERTICAL_INPUT_AXIS);
    }

    private void applySuspension()
    {
        Ray ray = new Ray(transform.position, -body.transform.up);

        if (Physics.Raycast(ray, out hit, SuspensionLength))
        {
            hadHitGround = true;
            float distance = SuspensionLength - hit.distance; //This gets the distance the "string was pushed" in the context of this implementation
                                                    //hit.distance is the raycast's distance to the hit point
            float force = -SuspensionStrength * distance + (Damping * car.GetPointVelocity(transform.position).y); //the -bv had to be inverted for damping to work.


            car.AddForceAtPosition(force * Time.fixedDeltaTime * -hit.normal, transform.position);

            return;
        }

        hadHitGround = false;
    }

    private void OnDrawGizmos()
    {
       
        if (hit.point != Vector3.zero)
        {
            Gizmos.DrawSphere(car.ClosestPointOnBounds(transform.position),0.1f);
            Gizmos.DrawLine(transform.position, hit.point);
        }
    }
}
