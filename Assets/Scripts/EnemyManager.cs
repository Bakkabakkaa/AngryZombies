using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyList;

    public int EnemyCount => _enemyList.Count;
    
    public event Action<int> EnemyCountChanged;
    public event Action AllEnemiesDied; 

    private void Start()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.OnEnemyDied += RemoveEnemyFromList;
        }
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        enemy.OnEnemyDied -= RemoveEnemyFromList;
        _enemyList.Remove(enemy);
        
        EnemyCountChanged?.Invoke(_enemyList.Count);
        
        if (_enemyList.Count == 0)
        {
            AllEnemiesDied?.Invoke();
        }
    }
}