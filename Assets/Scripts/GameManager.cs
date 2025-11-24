using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private Skull _skull;
    [SerializeField] private SlingshotController _slingshotController;
    [SerializeField] private SlingLinesController _slingLinesController;
    
    private EnemyManager _enemyManager;

    private void Awake()
    {
        _enemyManager = GetComponent<EnemyManager>();
    }

    private void OnEnable()
    {
        _enemyManager.AllEnemiesDied += ShowWinScreen;
        _skull.OnOutOfMap += ResetSkullPosition;
    }

    private void OnDisable()
    {
        _enemyManager.AllEnemiesDied -= ShowWinScreen;
        _skull.OnOutOfMap -= ResetSkullPosition;
    }

    private void ShowWinScreen()
    {
        _winScreen.SetActive(true);
    }

    private void ResetSkullPosition()
    {
        _slingshotController.ResetSkull();
        _slingLinesController.ResetLineRenderer();
    }
}