using UnityEngine;

public class ChaseState : EnemyState
{
    private readonly EnemyMover _enemyMovement;
    private readonly EnemyAttacker _enemyAttack;
    private readonly int _multiplieSpeed = 2;

    public ChaseState(Enemy enemy, EnemyMover enemyMovement, EnemyAttacker enemyAttack, IStateChanger stateChanger) : base(enemy, stateChanger)
    {
        _enemyAttack = enemyAttack;
        _enemyMovement = enemyMovement;
    }

    public override void Exit() =>
        _enemyMovement.ResetSpeed();

    public override void UpdatePhysicState()
    {
        if (Enemy.IsAggroed == false)
            StateChanger.SetState<PatrolState>();

        if (Enemy.IsAggroed && Mathf.Abs(Enemy.GetTargetPosition().x - Enemy.transform.position.x) <= _enemyAttack.AttackRange)
            StateChanger.SetState<CombatState>();

        _enemyMovement.SetTargetToMove(Enemy.GetTargetPosition(), _multiplieSpeed);
    }
}