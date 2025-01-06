using UnityEngine;

public class HealthKitSpawner : ItemSpawner
{
    [SerializeField] private HealthKit _healthKitPrefab;

    private void Awake()
    {
        ItemPrefab = _healthKitPrefab;
    }     
}