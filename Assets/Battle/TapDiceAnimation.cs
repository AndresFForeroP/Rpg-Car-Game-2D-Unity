using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TapDiceAnimation : MonoBehaviour
{
    void Start()
    {
        
        InvokeRepeating(nameof(Shake),1.5f,1.5f);
    }
    void Shake()
    {
        transform.DOShakeRotation(
            1,
            new Vector3(0, 0, 5)
        );
    }
}
