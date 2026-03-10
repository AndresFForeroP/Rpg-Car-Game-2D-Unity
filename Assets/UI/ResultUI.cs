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
    [SerializeField] GameObject GameUi;
    [SerializeField] Transition transition;
    [SerializeField] GameObject back;

    bool isTransitioning = false;

    void Update()
    {
        if (isTransitioning) return;

        if (playerCar.lap > 5 && playerCar.position == 1 || enemyCar.die)
        {
            isTransitioning = true;
            FinishRace();
            StartCoroutine(RestartGame());
            victory.SetActive(true);
        }
        else if (enemyCar.lap > 5 && enemyCar.position == 1 || playerCar.die)
        {
            isTransitioning = true;
            FinishRace();
            StartCoroutine(RestartGame());
            defeat.SetActive(true);
        }
    }

    public void FinishRace()
    {
        back.SetActive(true);
        StartCoroutine(transition.EndGame());
        gameObject.SetActive(true);
        GameUi.SetActive(false);
        enemyCar.agent.isStopped = true;
        playerCar.Speed = 0;
        playerCar.acceleration = 0;
        playerCar.steeringSpeed = 0;
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}