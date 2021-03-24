using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    public Rigidbody rb;
    public Transform CenterOfMass;
    public Transform forceObject;
  
    Vector2 input;
    public float Speed;
    public float grip;

    public float Torque;

    private void Start()
    {
        //ForwardAxis.Normalize();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.centerOfMass = CenterOfMass.localPosition;
        var locVel = transform.InverseTransformDirection(rb.velocity);
        rb.AddForce( -locVel.z * transform.forward, ForceMode.VelocityChange);

        TakeInput();
        Move();
    }

    private void Move()
    {
        rb.AddTorque(transform.up * input.x * Time.deltaTime * Torque);
       // rb.AddForce(rb.velocity.z * rb.mass * grip * transform.forward);
    }

    private void TakeInput()
    {
        input.x = Input.GetAxisRaw(Constants.HORIZONTAL_INPUT_AXIS);
        input.y = Input.GetAxisRaw(Constants.VERTICAL_INPUT_AXIS);
    }
}
