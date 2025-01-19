using System;
using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    private const KeyCode vampirismKey = KeyCode.E; 

    [SerializeField] private Player _player;
    [SerializeField] private Transform _vampirismPoint;
    [SerializeField] private DamageType _damageType;
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _duration = 6;
    [SerializeField] private float _detectorRadius;

    private bool _isActive = false;

    public event Action Activated;
    public event Action Reloading;

    public float Cooldown { get; private set; } = 4f;

    private void Awake()
    {
        _vampirismPoint.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(vampirismKey))
        {
            if (_isActive == false)
                StartCoroutine(SpellEffect(_duration));
        }
    }

    private IEnumerator SpellEffect(float actionTime)
    {
        float healAmount = (_damage / 2) * Time.deltaTime;

        Activated?.Invoke();
        _isActive = true;
        _vampirismPoint.gameObject.SetActive(true);

        while (actionTime > 0)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_vampirismPoint.position, _detectorRadius);

            foreach (Collider2D hit in hitEnemies)            
                if (hit.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_damage * Time.deltaTime, _damageType);
                    _player.RestoreHealth(healAmount);
                }         

            actionTime -= Time.deltaTime;

            yield return null;
        }

        _vampirismPoint.gameObject.SetActive(false);

        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        Reloading?.Invoke();

        yield return new WaitForSeconds(Cooldown);

        _isActive = false;
    }
}