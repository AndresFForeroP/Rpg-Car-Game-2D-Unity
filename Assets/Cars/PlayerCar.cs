using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : VehicleBase
{
    [SerializeField] float MaxSpeed = 20f;
    [SerializeField] public float acceleration = 0.1f;
    
    [SerializeField] public float Speed = 0;
    public float steeringSpeed = 200f;
    protected override void Die()
    {
        Debug.Log("murio");
    }

    void Update()
    {
        if (Speed != MaxSpeed || acceleration < 0)
        {
            Speed += acceleration;
        }
        if (Speed > MaxSpeed)
        {
            Speed = MaxSpeed;
        }
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, -steerInput * steeringSpeed * Time.deltaTime);
        if (acceleration < 0 && Speed <= 0)
        {
            Speed = 0;
            acceleration = 0;
        }
    }
}
