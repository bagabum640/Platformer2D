using UnityEngine;
using static AnimationsData;

[RequireComponent(typeof(PlayerGroundDetector),
                  typeof(PlayerMover),
                  typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerCombat),
                  typeof(BoxCollider2D),
                  typeof(Rigidbody2D))]
[RequireComponent(typeof(Health),
                  typeof(Animator))]
public class Player : MonoBehaviour
{ 
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private BoxCollider2D _boxCollider;

    private Animator _animator;
    private PlayerGroundDetector _groundCheck;
    private PlayerMover _movement;
    private PlayerCombat _combat;
    private PlayerInputReader _input;
    private Health _health;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _groundCheck = GetComponent<PlayerGroundDetector>();
        _movement = GetComponent<PlayerMover>();
        _combat = GetComponent<PlayerCombat>();
        _input = GetComponent<PlayerInputReader>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    private void FixedUpdate()
    {
        _movement.Move(_input.Direction);
        _movement.Fall();

        Attack();
        JumpDown();
        Jump();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
        _animator.SetTrigger(Hurt);
    }

    public void RestoreHealth(int healthAmount) =>
        _health.RestoreHealth(healthAmount);

    public bool GetPossibleOfHealing() =>
         _health.GetPossibleOfHealing();

    private void Attack()
    {
        if (_input.GetIsAttack() && _groundCheck.IsOnGround)
            _combat.TryToAttack();
    }

    private void JumpDown()
    {
        if (_input.GetIsJumpDown() && _groundCheck.IsOnGround)
            _movement.JumpDown();
    }

    private void Jump()
    {
        if (_input.GetIsJump() && _groundCheck.IsOnGround)
            _movement.Jump();
    }

    private void Die()
    {
        _animator.SetTrigger(Death);
        _boxCollider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        this.enabled = false;
    }
}