using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.CodeDom.Compiler;

public class Transition : MonoBehaviour
{
    [SerializeField] PlayerCar playerCar;
    [SerializeField] EnemyCar enemyCar;
    [SerializeField] GameObject victory;
    [SerializeField] GameObject defeat;
    [Header("UI")]
    public CanvasGroup transitionPanel;
    [SerializeField] GameObject GameUi;
    [SerializeField] GameObject Battleroot;
    [SerializeField] Dice dice;
    [SerializeField] EnemyDice enemyDice;
    [Header("Audio")]
    [SerializeField] AudioSource AudioRace;
    [SerializeField] AudioSource AudioBattle;

    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera Cinecam;
    [SerializeField] Transform targetToFollow;
    [SerializeField] Transform targetToFollowResultUI;
    [SerializeField] Transform targetToFollowRace;
    [SerializeField] GameObject CondicionalUi;
    [SerializeField] GameObject Playerturn;
    [SerializeField] GameObject Enemyturn;

    [Header("Settings")]
    public float fadeDuration = 0.4f;
    public float blackScreenTime = 0.3f;

    bool isTransitioning = false;

    void Update()
    {
        if (isTransitioning) return;

        if ((playerCar.lap > 5 && playerCar.position == 1) || enemyCar.die)
        {
            isTransitioning = true;
            StartCoroutine(EndGame(true));
        }
        else if ((enemyCar.lap > 5 && enemyCar.position == 1) || playerCar.die)
        {
            isTransitioning = true;
            StartCoroutine(EndGame(false));
        }
    }
    IEnumerator EndGame(bool isVictory)
    {
        GameUi.SetActive(false);


        playerCar.Speed = 0;
        playerCar.acceleration = 0;
        playerCar.steeringSpeed = 0;

        if (enemyCar.agent != null)
            enemyCar.agent.isStopped = true;

        Sequence seq = DOTween.Sequence();


        seq.Append(transitionPanel
            .DOFade(1f, fadeDuration)
            .SetEase(Ease.InOutSine));

        seq.AppendCallback(() =>
        {
            Cinecam.Follow = targetToFollowResultUI;

            if (isVictory)
            {
                victory.SetActive(true);
                Debug.Log("ganaste");
            }
            else
            {
                defeat.SetActive(true);
                Debug.Log("perdiste");
            }
        });


        seq.AppendInterval(blackScreenTime);

 
        seq.Append(transitionPanel
            .DOFade(0f, fadeDuration)
            .SetEase(Ease.InOutSine));

        seq.OnComplete(() =>
        {
            StartCoroutine(RestartGame());
        });

        yield return null;
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToBattle()
    {
        AudioRace.Stop();
        AudioBattle.Play();
        dice.Initial();
        enemyDice.Initial();
        Playerturn.SetActive(true);
        Enemyturn.SetActive(true);
        if (isTransitioning) return;
        isTransitioning = true;

        GameUi.SetActive(false);

        Sequence seq = DOTween.Sequence();

        seq.Append(transitionPanel
            .DOFade(1f, fadeDuration)
            .SetEase(Ease.InOutSine));

        seq.AppendCallback(() =>
        {
            Debug.Log("Entrando en batalla");

            Cinecam.Follow = targetToFollow;
            Cinecam.OnTargetObjectWarped(
                targetToFollow,
                targetToFollow.position - Cinecam.transform.position
            );
        });

        seq.AppendInterval(blackScreenTime);
        seq.Append(transitionPanel
            .DOFade(0f, fadeDuration)
            .SetEase(Ease.InOutSine));

        seq.OnComplete(() =>
        {
            GameUi.SetActive(true);
            isTransitioning = false;
        });
    }

    public void GoToRace()
    {
        
        AudioBattle.Stop();
        AudioRace.Play();
        CondicionalUi.SetActive(false);
        if (isTransitioning) return;
        isTransitioning = true;
        GameUi.SetActive(false);

        Sequence seq = DOTween.Sequence();

        seq.Append(transitionPanel
            .DOFade(1f, fadeDuration)
            .SetEase(Ease.InOutSine));
        seq.AppendCallback(() =>
        {
            Debug.Log("Volviendo a la carrera");

            Cinecam.Follow = targetToFollowRace;
            Cinecam.OnTargetObjectWarped(
                targetToFollowRace,
                targetToFollowRace.position - Cinecam.transform.position
            );
        });

        seq.AppendInterval(blackScreenTime);

        seq.Append(transitionPanel
            .DOFade(0f, fadeDuration)
            .SetEase(Ease.InOutSine));

        seq.OnComplete(() =>
        {
            GameUi.SetActive(true);
            isTransitioning = false;
        });
    }
}