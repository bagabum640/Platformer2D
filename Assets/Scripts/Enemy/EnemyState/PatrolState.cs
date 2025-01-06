using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    private readonly EnemyMover _enemyMover;
    private readonly List<Vector3> _path = new();
    private readonly float _distanceToPoint = 1f;

    private int _pointNumber;

    public PatrolState(Enemy enemy, EnemyMover enemyMover,IStateChanger stateChanger) : base(enemy,stateChanger)
    {
        _enemyMover = enemyMover;

        PathInit(_enemyMover.GetPointsPosition());
    }

    public override void PhysicUpdate()
    {
        if (Enemy.IsAggroed)        
            StateChanger.SetState<ChaseState>();
        
        if (Mathf.Abs(_path[_pointNumber].x - Enemy.transform.position.x) <= _distanceToPoint)
        {
            _pointNumber = ++_pointNumber % _path.Count;
        }

        _enemyMover.SetTargetToMove(_path[_pointNumber]);
    }

    private void PathInit(List<Vector3> points)
    {
        if (points.Count > 0)
        {
            foreach (Vector3 point in points)
                _path.Add(point);

            _path[_pointNumber] = points[_pointNumber];
        }
        else
        {
            _path[_pointNumber] = Enemy.transform.position;
        }
    }
}