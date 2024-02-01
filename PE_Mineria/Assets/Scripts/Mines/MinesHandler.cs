using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesHandler : MonoBehaviour
{
    private int _currentLevelMine = 0;
    private int _currenMine = 0;
    private bool _finishedMines;
    
    private Mine[] _mines;
    private void OnDisable()
    {
        GameManager.Instance.CycleManager.dayChangedEvent -= DayPassed;
    }
    public void InitializeMines()
    {
        GameManager.Instance.CycleManager.dayChangedEvent += DayPassed;
        
        _currentLevelMine = 0;
        _currenMine = 0;
        
        InitializeLevelMines();
    }

    private void InitializeLevelMines()
    {
        int children = transform.childCount;
        
        if (_currentLevelMine >= children || _currentLevelMine < 0)
        {
            Debug.LogWarning("Error id not valid");
            _finishedMines = true;
            return;
        }

        _mines = transform.GetChild(_currentLevelMine).gameObject.GetComponentsInChildren<Mine>();

        for (int i = 0; i < _mines.Length; i++)
        {
            Mine mine = _mines[i];

            if (i == 0)
            {
                mine.InitializeMine(ResourcesFactory.GetResource(0), true, GetComponent<UIMineHandler>());
                continue;
            } 
            
            mine.InitializeMine(ResourcesFactory.GetResource(), false, GetComponent<UIMineHandler>());
        }
    }
    
    private void DayPassed(int currentday)
    {
        if(_finishedMines) return;
        _currenMine += 1;

        if (_currenMine >= _mines.Length)
        {
            _currentLevelMine += 1;
            _currenMine = 0;
            
            int children = transform.childCount;
            
            if (_currentLevelMine >= children || _currentLevelMine < 0)
            {
                _finishedMines = true;
                return;
            }
        }
        
        _mines[_currenMine].UnlockMine();
    }
}
