using UnityEngine;

[RequireComponent(typeof(PlayerGroundDetector),
                  typeof(PlayerMover),
                  typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerCombat),
                  typeof(BoxCollider2D),
                  typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealthAmount = 10;
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
        _input = GetComponent<PlayerInputReader>();
        _movement = GetComponent<PlayerMover>();
        _groundCheck = GetComponent<PlayerGroundDetector>();
        _combat = GetComponent<PlayerCombat>();

        _health = new Health(_maxHealthAmount, _animator);
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

    public void TakeDamage(int damage) =>
        _health.TakeDamage(damage);

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
        _boxCollider.enabled = false;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        this.enabled = false;
    }
}