using UnityEngine;

public class PlayerItemPicker : MonoBehaviour
{
    private Player _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Item>(out Item item))
        {
            if (item is HealthKit healthKit)
            {
                if (_playerHealth.GetPossibleOfHealing())
                {
                    healthKit.Collect();
                    _playerHealth.RestoreHealth(healthKit.Amount);
                }
            }

            if (item is Coin coin)
            {
                coin.Collect();
            }
        }
    }
}