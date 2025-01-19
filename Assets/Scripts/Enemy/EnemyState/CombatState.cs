using UnityEngine;
using static AnimationsData;

public class CombatState : EnemyState
{
    private readonly Animator _animator;
    private readonly EnemyAttacker _enemyAttack;
    private readonly EnemyMover _enemyMovement;
    
    public CombatState(Enemy enemy, Animator animator, EnemyMover enemyMovement, EnemyAttacker enemyAttack, IStateChanger stateChanger) : base(enemy, stateChanger)
    {
        _animator = animator;
        _enemyAttack = enemyAttack;
        _enemyMovement = enemyMovement;
    }

    public override void Update()
    {
        if (Enemy.IsAggroed && Mathf.Abs(Enemy.GetTargetPosition().x - Enemy.transform.position.x) > _enemyAttack.AttackRange && _enemyMovement.CanMove)
            StateChanger.SetState<ChaseState>();

        if (Enemy.IsAggroed == false)
            StateChanger.SetState<PatrolState>();
        
        if (_enemyAttack.CanAttack)
        {            
            _enemyMovement.ProhibitMove();
            _animator.SetTrigger(Attack);         
            _enemyAttack.ResetTimerAttack();
        }        
    }
}