using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private DamageType _damageType;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _attackResetTime;

    private readonly float _startIndexAttack = 1;
    private readonly float _maxCountAttack = 3;

    private PlayerAnimations _playerAnimations;

    private float _currentIndexAttack;
    private float _timer;

    private void Awake() =>   
        _playerAnimations = GetComponent<PlayerAnimations>();    

    private void Update() =>
        _timer += Time.deltaTime;

    public void TryToAttack()
    {
        if (_timer >= _attackDelay)
        {
            _currentIndexAttack++;

            if (_currentIndexAttack > _maxCountAttack || _timer > _attackResetTime)
            {
                _currentIndexAttack = _startIndexAttack;
            }

            _playerAnimations.AttackAnimation(_currentIndexAttack);

            _timer = 0;
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D hit in hitEnemies)
            if (hit.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_damage, _damageType);
    }

    private void OnDrawGizmosSelected() =>
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
}