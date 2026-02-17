using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerCar : VehicleBase
{
    [SerializeField] float MaxSpeed = 25f;
    [SerializeField] public float acceleration = 0;
    [SerializeField] public float Speed = 0;
    [SerializeField] GameUIScript gameUIScript;
    [SerializeField] Animator playeranimator;
    public bool die = false;

    public float steeringSpeed = 0;

    protected override void Die()
    {
        die = true;
        Debug.Log("murio");
        Speed = 0;
        acceleration = 0;
        steeringSpeed = 0;
    }

    void Update()
    {
        if (Speed != MaxSpeed || acceleration < 0)
            Speed += acceleration;

        if (Speed > MaxSpeed)
            Speed = MaxSpeed;

        transform.Translate(Vector2.up * Speed * Time.deltaTime);

        float steerInput = GetSteerInput();
        transform.Rotate(0, 0, -steerInput * steeringSpeed * Time.deltaTime);

        if (acceleration < 0 && Speed <= 0)
        {
            Speed = 0;
            acceleration = 0;
        }
    }

    float GetSteerInput()
    {
        float steer = Input.GetAxis("Horizontal");

    #if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
                steer = -1f;
            else
                steer = 1f;
        }
    #else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary ||
                touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < Screen.width / 2)
                    steer = -1f;
                else
                    steer = 1f;
            }
        }
    #endif

        return steer;
    }
    public override void Nitro()
    {
        acceleration = 0.12f;
        MaxSpeed = 30;
        steeringSpeed = 200;
        playeranimator.SetBool("Nitro", true);
        StartCoroutine(brake());
    }

    IEnumerator brake()
    {
        yield return new WaitForSeconds(5f);
        acceleration = 0.8f;
        MaxSpeed = 25;
        playeranimator.SetBool("Nitro", false);
    }

    public override void Repair()
    {
        if (Repairs == 0)
        {
            Die();
        }

        playeranimator.SetBool("Repair", true);
        Repairs--;
        StartCoroutine(WaitToRepair());
    }

    IEnumerator WaitToRepair()
    {
        yield return new WaitForSeconds(3f);
        acceleration = 0.8f;
        MaxSpeed = 25;
        steeringSpeed = 200;
        playeranimator.SetBool("Repair", false);
    }
}