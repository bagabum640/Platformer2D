using System;
using UnityEngine;
using static AnimationsData;

public class Health
{
    private readonly Animator _animator;
    private readonly int _maxAmount;
    private readonly int _minAmount = 0;

    private int _currentAmount;

    public bool IsAlive { get; private set; } = true;

    public event Action Died;

    public Health(Animator animator,int maxHealthAmount)
    {
        _animator = animator;
        _maxAmount = maxHealthAmount;
        _currentAmount = _maxAmount;
    }

    public void TakeDamage(int damage)
    {
        if (damage >= 0)
        {
            _currentAmount = Mathf.Clamp(_currentAmount - damage, _minAmount, _maxAmount);
        }

        if (_currentAmount <= 0)
        {
            IsAlive = false;
            Died?.Invoke();
            _animator.SetTrigger(Death);
        }
        else
        {
            _animator.SetTrigger(Hurt);
        }
    }

    public void RestoreHealth(int healthAmount) =>
       _currentAmount = Mathf.Clamp(_currentAmount + Mathf.Abs(healthAmount), _minAmount, _maxAmount);

    public bool GetPossibleOfHealing() =>
        _currentAmount < _maxAmount;
}