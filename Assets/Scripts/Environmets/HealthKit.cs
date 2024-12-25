using UnityEngine;

public class HealthKit : Item
{
    [SerializeField] private int _healthAmount = 3;

    private void Awake()
    {
        Amount = _healthAmount;
    }
}