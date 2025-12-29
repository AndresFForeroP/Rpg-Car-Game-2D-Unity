using UnityEngine;

public abstract class VehicleBase : MonoBehaviour
{
    [SerializeField] protected int HP = 100;
    protected abstract void Die();
}
