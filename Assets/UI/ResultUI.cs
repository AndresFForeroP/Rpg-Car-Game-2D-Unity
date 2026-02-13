using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ResultUI : MonoBehaviour
{
    [SerializeField] PlayerCar playerCar;
    [SerializeField] EnemyCar enemyCar;
    [SerializeField] GameObject victory;
    [SerializeField] GameObject defeat;
    [SerializeField] CinemachineVirtualCamera Cinecam;
    [SerializeField] Transform targetToFollow;
    [SerializeField] GameObject GameUi;

    bool isTransitioning = false;

    void Update()
    {
        if (isTransitioning) return;

        if (playerCar.lap > 5 && playerCar.position == 1 || enemyCar.die)
        {
            isTransitioning = true;

            GameUi.SetActive(false);
            playerCar.Speed = 0;
            playerCar.acceleration = 0;
            playerCar.steeringSpeed = 0;
            victory.SetActive(true);

            Debug.Log("ganaste");

            StartCoroutine(RestartGame());
        }
        else if (enemyCar.lap > 5 && enemyCar.position == 1 || playerCar.die)
        {
            isTransitioning = true;

            GameUi.SetActive(false);
            Debug.Log("perdiste");
            defeat.SetActive(true);
            enemyCar.agent.isStopped = true;

            StartCoroutine(RestartGame());
        }
    }


    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}