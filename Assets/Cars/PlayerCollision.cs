using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCollision : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerCar player;
    [SerializeField] Transition transition;
    [SerializeField] AudioSource sonido;
    [SerializeField] GameObject battleUI;
    [SerializeField] SpriteRenderer playerSprite;

    [Header("Invincibility")]
    [SerializeField] float invincibleTime = 7f;
    [SerializeField] float blinkRate = 0.15f;

    bool isInvincible = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        player.Speed *= 0.5f;

        if (isInvincible) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Chocaste con enemigo");

            sonido.Play();
            battleUI.SetActive(true);

            player.acceleration = -0.10f;
            player.steeringSpeed = 0;

            NavMeshAgent enemyAgent = collision.gameObject.GetComponent<NavMeshAgent>();
            if (enemyAgent != null)
                enemyAgent.isStopped = true;
                enemyAgent.velocity = enemyAgent.velocity/2;

            transition.GoToBattle();

        }
    }
    public void invencibility()
    {
        StartCoroutine(InvincibilityCoroutine());
    }
    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");


        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        float elapsed = 0f;

        while (elapsed < invincibleTime)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(blinkRate);

            playerSprite.enabled = true;
            yield return new WaitForSeconds(blinkRate);

            elapsed += blinkRate * 2;
        }

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);

        playerSprite.enabled = true;
        isInvincible = false;
    }
}