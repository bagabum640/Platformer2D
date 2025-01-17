using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const int MinAmount = 0;

    [field: SerializeField] public int MaxAmount { get; private set; } = 10;
    [field: SerializeField] public int CurrentAmount { get; private set; }
    public bool IsAlive { get; private set; } = true;

    public event Action ValueChanged;

    public event Action Died;

    private void Awake()
    {
        CurrentAmount = MaxAmount;
    }

    private void Start()
    {
        ValueChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (damage >= 0)
        {
            CurrentAmount = Mathf.Clamp(CurrentAmount - damage, MinAmount, MaxAmount);
        }

        if (CurrentAmount <= 0)
        {
            IsAlive = false;
            Died?.Invoke();
        }

        ValueChanged?.Invoke();
    }

    public void Restore(int amount)
    {
        CurrentAmount = Mathf.Clamp(CurrentAmount + Mathf.Abs(amount), MinAmount, MaxAmount);

        ValueChanged?.Invoke();
    }

    public bool GetPossibleOfHealing() =>
        CurrentAmount < MaxAmount;
}