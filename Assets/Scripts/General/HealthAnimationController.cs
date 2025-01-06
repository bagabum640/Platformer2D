using UnityEngine;
using static AnimationsData;

public class HealthAnimationController : IHealthObserver
{
    private readonly Animator _animator;

    public HealthAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void OnHealthChanged(int currentHealth)
    {
        if (currentHealth <= 0)
            _animator.SetTrigger(Death);
        else
            _animator.SetTrigger(Hurt);
    }
}