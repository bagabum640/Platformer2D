using UnityEngine;
using static AnimationsData;

[RequireComponent(typeof(EnemyMover),
                  typeof(Animator),
                  typeof(EnemyAttacker))]
[RequireComponent(typeof(BoxCollider2D),
                  typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private AggroDetector _aggroDetector;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Transform _target;
    private Animator _animator;
    private EnemyMover _enemyMovement;
    private EnemyAttacker _enemyAttack;
    private EnemyStateMachine _stateMachine;
    private Health _health;

    public bool IsAggroed { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyMovement = GetComponent<EnemyMover>();
        _enemyAttack = GetComponent<EnemyAttacker>();
        _health = GetComponent<Health>();

        _stateMachine = new EnemyStateMachine(this, _animator, _enemyMovement, _enemyAttack);
        _stateMachine.SetState<PatrolState>();
    }

    private void OnEnable()
    {
        _aggroDetector.TargetFound += SetTarget;
        _aggroDetector.TargetLost += LossOfTarget;
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _aggroDetector.TargetFound -= SetTarget;
        _aggroDetector.TargetLost -= LossOfTarget;
        _health.Died -= Die;
    }

    private void Update()
    {
        if (_health.IsAlive)
            _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        if (_health.IsAlive)
            _stateMachine.FixedUpdate();
    }

    public void TakeDamage(int damage)
    {
        _animator.SetTrigger(Hurt);
        _health.TakeDamage(damage);
    }

    public Vector3 GetTargetPosition()
    {
        if (_target != null)
            return _target.position;

        return Vector3.zero;
    }

    private void SetTarget(Transform target)
    {
        _target = target;
        IsAggroed = true;
    }

    private void LossOfTarget()
    {
        _target = null;
        IsAggroed = false;
    }

    private void Die()
    {
        _animator.SetTrigger(Death);
        _boxCollider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
    }
}