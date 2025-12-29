using UnityEngine;

public abstract class VehicleBase : MonoBehaviour
{
    [SerializeField] protected int Damage = 0;
    protected abstract void Die();
}
