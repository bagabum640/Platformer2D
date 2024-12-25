using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AnimationsData;

[RequireComponent(typeof(Rigidbody2D),
                  typeof(DirectionFlipper),
                  typeof(Animator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _stopMoveDelay;

    private DirectionFlipper _directionFlipper;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private WaitForSeconds _stopMoveTime;
   
    public bool CanMove { get;private set; } = true;
    public float GetCurrentSpeed => _rigidbody.velocity.x;

    private void Awake()
    {
        _directionFlipper = GetComponent<DirectionFlipper>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _stopMoveTime = new(_stopMoveDelay);
    }

    public void SetTargetToMove(Vector3 target, float multiplieSpeed = 1f)
    {
        _directionFlipper.SetDirection(target.x - transform.position.x);

        _animator.SetFloat(MovementSpeed, Mathf.Abs(GetCurrentSpeed));

        _rigidbody.velocity = (target - transform.position).normalized * (_speed * multiplieSpeed) * Vector2.right;
    }

    public List<Vector3> GetPointsPosition() =>
        _points.Select(point => point.position).ToList(); 

    public void ResetSpeed() =>
        _rigidbody.velocity = Vector3.zero;

    public void ProhibitMove() =>
        StartCoroutine(StopMove());

    private IEnumerator StopMove()
    {
        CanMove = false;

        yield return _stopMoveTime;

        CanMove = true;
    }
}