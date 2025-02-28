using UnityEngine;
using static AnimationsData;

public class PlayerAnimations : MonoBehaviour
{
    private readonly int _jumping = Animator.StringToHash("Jumping");
    private readonly int _falling = Animator.StringToHash("Falling");

    private Animator _playerAnimator;

    private void Awake() =>
        _playerAnimator = GetComponent<Animator>();

    public void MoveAnimation(float direction) =>
        _playerAnimator.SetFloat(MovementSpeed, Mathf.Abs(direction));

    public void JumpAnimation() =>
        _playerAnimator.SetTrigger(_jumping);

    public void FallAnimation(float velocity) =>
        _playerAnimator.SetFloat(_falling, velocity);

    public void AttackAnimation(float currentAttack) =>
        _playerAnimator.SetTrigger(Attack + currentAttack.ToString("0"));     

    public void HurtAnimation() =>
        _playerAnimator.SetTrigger(Hurt);

    public void DeathAnimation() =>
        _playerAnimator.SetTrigger(Death);
}