using UnityEngine;

public abstract class VehicleBase : MonoBehaviour
{
    [SerializeField] public int Repairs = 3;
    [SerializeField] public int lap = 1;
    [SerializeField] public int position = 1;
    protected abstract void Die();
    public abstract void Nitro();
    public abstract void Repair();
}
