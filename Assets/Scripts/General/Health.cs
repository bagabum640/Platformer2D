using System;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private readonly List<IHealthObserver> _observers = new();
    private readonly int _maxAmount;
    private readonly int _minAmount = 0;

    private int _currentAmount;

    public bool IsAlive { get; private set; } = true;

    public event Action Died;

    public Health(int maxHealthAmount, Animator animator)
    {
        _maxAmount = maxHealthAmount;
        _currentAmount = _maxAmount;

        AddObserver(new HealthAnimationController(animator));
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
        }

        NotifyObservers();
    }

    public void RestoreHealth(int healthAmount) =>
       _currentAmount = Mathf.Clamp(_currentAmount + Mathf.Abs(healthAmount), _minAmount, _maxAmount);

    public bool GetPossibleOfHealing() =>
        _currentAmount < _maxAmount;

    public void AddObserver(IHealthObserver observer) =>
        _observers.Add(observer);

    public void RemoveObserver(IHealthObserver observer) =>
        _observers.Remove(observer);

    private void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.OnHealthChanged(_currentAmount);
        }
    }
}