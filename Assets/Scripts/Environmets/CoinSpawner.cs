using UnityEngine;

public class CoinSpawner : ItemSpawner
{
    [SerializeField] private Coin _coinPrefab;

    private void Awake()
    {
        ItemPrefab = _coinPrefab;
    }
}