using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TapDice : MonoBehaviour
{
    [SerializeField] Button buttonTapDice;
    [SerializeField] Dice dice;
    public int result;
    void Start()
    {
        buttonTapDice.onClick.AddListener(tapDice);
    }
    public void tapDice()
    {
        result = dice.RollDice();
    }

}
