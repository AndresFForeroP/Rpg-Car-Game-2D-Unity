using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCar : VehicleBase
{
    public Transform[] points;
    private int currentPointIndex;
    public NavMeshAgent agent;
    [SerializeField] Animator EnemyAnimator;
    public float rotationSpeed = 5f;
    public bool die = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.autoBraking = false;
        agent.angularSpeed= 0;
        agent.SetDestination(points[currentPointIndex].position);
        agent.isStopped = true;
    }
    void Update()
    {
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            float angle = (Mathf.Atan2(agent.velocity.y, agent.velocity.x) * Mathf.Rad2Deg) -90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (!agent.pathPending && agent.remainingDistance < 10f)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length;
            agent.SetDestination(points[currentPointIndex].position);
            agent.velocity = agent.velocity/1.5f;
            agent.acceleration = 20;
        }
    }
    protected override void Die()
    {
        die = true;
        Debug.Log("enemigo destruido");
        agent.isStopped = true;
    }

    public override void Nitro()
    {
        agent.isStopped = false;
        agent.speed = 30;
        agent.acceleration = 10;
        StartCoroutine(brake());
        EnemyAnimator.SetBool("Nitro",true);
    }
    IEnumerator brake()
    {
        yield return new WaitForSeconds(5f);
        agent.acceleration = 7.1f;
        agent.speed = 25;
        agent.velocity = agent.velocity/1.4f;
        EnemyAnimator.SetBool("Nitro",false);
    }


    public override void Repair()
    {
        if (Repairs == 0)
        {
            Die();
        }
        else
        {
            EnemyAnimator.SetBool("Repair",true);
            Repairs--;
            StartCoroutine(WaitToRepair());
        }
    }
    IEnumerator WaitToRepair()
    {
        yield return new WaitForSeconds(3f);
        agent.isStopped = false;
        agent.acceleration = 7.1f;
        agent.speed = 25;
        EnemyAnimator.SetBool("Repair",false);
    }
}
