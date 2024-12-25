using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private int _damage = 2;

    private readonly float _timerBetweenAttack = 1f;
    private float _attackDelay;

    [field: SerializeField] public float AttackRange { get; private set; }
    public bool CanAttack => _attackDelay >= _timerBetweenAttack;

    private void Update() =>
        _attackDelay += Time.deltaTime;

    public void ResetTimerAttack() =>
        _attackDelay = 0;

    private void Attack()  //Called via Event in Animation
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_weaponPoint.transform.position, AttackRange);

        for (int i = 0; i < hitPlayer.Length; i++)
            if (hitPlayer[i].TryGetComponent<Player>(out Player player))
                player.TakeDamage(_damage);
    }

    private void OnDrawGizmosSelected() =>
        Gizmos.DrawWireSphere(_weaponPoint.transform.position, AttackRange);
}