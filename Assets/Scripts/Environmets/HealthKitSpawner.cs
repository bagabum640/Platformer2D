using UnityEngine;

public class HealthKitSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private HealthKit _healthKitPrefab;

    private void Awake() =>
        Spawn();

    private void Spawn()
    {
        if (_spawnPoints.Length > 0)
            for (int i = 0; i < _spawnPoints.Length; i++)            
               Instantiate(_healthKitPrefab, _spawnPoints[i].position, Quaternion.identity, transform);            
    }
}