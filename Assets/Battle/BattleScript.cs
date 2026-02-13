
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    [SerializeField] GameObject EnemyDice;
    [SerializeField] EnemyDice EnemyDiceScript;
    [SerializeField] GameObject PlayerTurn;
    [SerializeField] TapDice tapdice;
    [SerializeField] TextMeshProUGUI condicionaltext;
    [SerializeField] Animator playanimator;
    [SerializeField] Animator enemyanimator;
    [SerializeField] Transition transition;
    [SerializeField] PlayerCar playerCar;
    [SerializeField] EnemyCar enemyCar;
    [SerializeField] PlayerCollision playerCollision;
    public void battle()
    {
        PlayerTurn.SetActive(false);
        EnemyDice.SetActive(true);
        int resultenemy = EnemyDiceScript.RollDice();
        comparedices(tapdice.result,resultenemy);
    }
    public void comparedices(int playerresult,int enemyresult)
    {
        Debug.Log(playerresult);
        Debug.Log(enemyresult);
        if (playerresult > enemyresult)
        {
            condicionaltext.text="WIN";
            StartCoroutine(PlayAnimationPlayerAfterDelay("PlayerWin"));
            StartCoroutine(PlayAnimationEnemyAfterDelay("EnemyLose"));
            StartCoroutine(ReturnTorace());
            StartCoroutine(WinPlayer());

        }
        else if( enemyresult > playerresult)
        {
            condicionaltext.text="LOSE";
            StartCoroutine(PlayAnimationPlayerAfterDelay("PlayerLose"));
            StartCoroutine(PlayAnimationEnemyAfterDelay("EnemyWin"));
            StartCoroutine(ReturnTorace());
            StartCoroutine(LosePlayer());
        }
        else
        {
            condicionaltext.text="DRAW";
            StartCoroutine(PlayAnimationPlayerAfterDelay("Draw"));
            StartCoroutine(PlayAnimationEnemyAfterDelay("Draw"));
            StartCoroutine(ReturnTorace());
            StartCoroutine(Draw());
        }
    }
    IEnumerator Draw()
    {
        yield return new WaitForSeconds(10f);
        playerCar.Repair();
        enemyCar.Repair();
    }
    IEnumerator WinPlayer()
    {
        yield return new WaitForSeconds(10f);
        playerCar.Nitro();
        enemyCar.Repair();
    }
    IEnumerator LosePlayer()
    {
        yield return new WaitForSeconds(10f);
        playerCar.Repair();
        enemyCar.Nitro();
    }
    IEnumerator ReturnTorace()
    {
        yield return new WaitForSeconds(9f);
        transition.GoToRace();
        playerCollision.invencibility();
    }
    IEnumerator PlayAnimationPlayerAfterDelay(string animName)
    {
        yield return new WaitForSeconds(6f);
        playanimator.SetTrigger(animName);
        
    }
    IEnumerator PlayAnimationEnemyAfterDelay(string animName)
    {
        yield return new WaitForSeconds(6f);
        enemyanimator.SetTrigger(animName);
    }

}
