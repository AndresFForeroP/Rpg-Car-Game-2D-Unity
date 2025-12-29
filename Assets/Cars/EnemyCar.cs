using UnityEngine;
using UnityEngine.AI;

public class EnemyCar : VehicleBase
{
    public Transform[] points;
    private int currentPointIndex;
    private NavMeshAgent agent;
    public float rotationSpeed = 5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.autoBraking = false;
        agent.angularSpeed= 0;
        agent.SetDestination(points[currentPointIndex].position);
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            agent.isStopped = true;
        }
    }
    protected override void Die()
    {
        Debug.Log("enemigo destruido");
    }
}
