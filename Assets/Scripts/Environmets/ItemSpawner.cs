using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    protected Item ItemPrefab;

    private void Start() =>
        Spawn();

    private void Spawn()
    {
        if (_spawnPoints.Length > 0)
            for (int i = 0; i < _spawnPoints.Length; i++)
                Instantiate(ItemPrefab, _spawnPoints[i].position, Quaternion.identity, transform);
    }
}