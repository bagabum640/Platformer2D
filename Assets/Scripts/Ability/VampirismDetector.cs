using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismDetector : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;

    public List<Enemy> EnemyDetected { get; private set; } = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy) && EnemyDetected.Contains(enemy) == false)
        {
            EnemyDetected.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            EnemyDetected.Remove(enemy);
        }
    }
}