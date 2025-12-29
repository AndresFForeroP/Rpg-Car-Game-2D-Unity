using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] PlayerCar player;
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        player.Speed /= 2;
        // player.acceleration = -0.1f;
        Debug.Log("choco");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            player.acceleration = 0;
            NavMeshAgent enemyAgent = collision.gameObject.GetComponent<NavMeshAgent>();
            player.Speed = 0;  
            enemyAgent.isStopped = true;
            Debug.Log("Chocaste con enemigo");
        }
    }
}
