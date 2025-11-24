using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    private EnemyManager _enemyManager;
    
    private void Awake()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _enemyCountText.text = _enemyManager.EnemyCount.ToString();
    }

    private void OnEnable()
    {
        _enemyManager.EnemyCountChanged += UpdateEnemyText;
    }

    private void OnDisable()
    {
        _enemyManager.EnemyCountChanged += UpdateEnemyText;
    }

    private void UpdateEnemyText(int count)
    {
        _enemyCountText.text = count.ToString();
    }
}