using UnityEngine;

public class ItemSpawner<TItem> : MonoBehaviour where TItem : Item
{
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private TItem _itemPrefab;

    private void Start() =>
        Spawn();

    public void SetItemPrefab(TItem prefab) =>
        _itemPrefab = prefab;

    private void Spawn()
    {
        if (_spawnPoints.Length > 0)
            for (int i = 0; i < _spawnPoints.Length; i++)
                Instantiate(_itemPrefab, _spawnPoints[i].position, Quaternion.identity, transform);
    }
}