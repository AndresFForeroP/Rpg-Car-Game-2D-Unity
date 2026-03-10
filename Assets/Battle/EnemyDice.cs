using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyDice : MonoBehaviour
{
    public Sprite[] diceFaces;
    SpriteRenderer spriteRenderer;
    Animator animator;
    [SerializeField] GameObject CondicionalUi;
    [SerializeField] GameObject Enemyturn;

    int lastResult;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    public void Initial()
    {
        transform.DOLocalMove(new Vector3(922,-1615.8f,0),0.1f);
        transform.DOScale(1.12f,1);
        gameObject.SetActive(false);
    }

    public int RollDice()
    {
        lastResult = Random.Range(0, diceFaces.Length);

        animator.enabled = true;
        animator.Play("DiceAnimation", 0, 0);

        StartCoroutine(ShowResultAfterAnimation());

        return lastResult + 1;
    }

    IEnumerator ShowResultAfterAnimation()
    {
        yield return new WaitForSeconds(2f);
        animator.enabled = false;
        spriteRenderer.sprite = diceFaces[lastResult];
        PlayResultAnimation();
    }
    void PlayResultAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.8f, 0.25f).SetEase(Ease.OutBack))
           .Append(transform.DOScale(1f, 0.15f).SetEase(Ease.InOutQuad))
           .AppendInterval(1f)
           .Append(transform.DOScale(0.5f, 1f).SetEase(Ease.InOutQuad))
           .Append(transform.DOLocalMove( new Vector3(926.19f,-1612.75f,0), 1))
           .AppendInterval(0.5f)
           .OnComplete(() =>
            {
                CondicionalUi.SetActive(true);
                Enemyturn.SetActive(false);
            });
    }
}
