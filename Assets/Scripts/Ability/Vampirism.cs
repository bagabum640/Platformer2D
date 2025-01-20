using System;
using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private VampirismDetector _vampirismDetector;
    [SerializeField] private Player _player;
    [SerializeField] private DamageType _damageType;
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _multiplierHeal = 0.5f;
    [SerializeField] private float _duration = 6;
    [SerializeField] private float _detectorRadius;

    private WaitForSeconds _cooldownTime;
    private bool _isActive = false;

    public event Action Activated;
    public event Action Reloading;

    public float Cooldown { get; private set; } = 4f;

    private void Awake()
    {
        _cooldownTime = new(Cooldown);
        _vampirismDetector.gameObject.SetActive(false);
    }

    public void Active()
    {
        if (_isActive == false)
            StartCoroutine(SpellEffect(_duration));
    }

    public Enemy GetEnemy()
    {
        Enemy target = null;
        float shortestDistance = float.MaxValue;

        foreach(Enemy enemy in _vampirismDetector.EnemyDetected)
        {
            if(enemy == null)
                continue;

            float distance = Mathf.Abs(enemy.transform.position.x - transform.position.x);

            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                target = enemy;
            }
        }

        return target;
    }

    private IEnumerator SpellEffect(float actionTime)
    {
        float transfusionAmount = _damage * Time.deltaTime;

        Activated?.Invoke();
        _isActive = true;
        _vampirismDetector.gameObject.SetActive(true);

        while (actionTime > 0)
        {
            Enemy enemy = GetEnemy();

            if ((enemy != null))
            {
                enemy.TakeDamage(transfusionAmount, _damageType);
                _player.RestoreHealth(transfusionAmount * _multiplierHeal);
            }          

            actionTime -= Time.deltaTime;

            yield return null;
        }

        _vampirismDetector.gameObject.SetActive(false);

        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        Reloading?.Invoke();

        yield return _cooldownTime;

        _isActive = false;
    }
}