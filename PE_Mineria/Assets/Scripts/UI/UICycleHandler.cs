using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICycleHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentDayCounter;
    [SerializeField] private TextMeshProUGUI _currentDayCycle;

    private void OnEnable()
    {
        GameManager.Instance.CycleManager.dayCycleChangedEvent += UpdateDayCycle;
        GameManager.Instance.CycleManager.dayChangedEvent += UpdateDayCounter;
    }

    private void OnDisable()
    {
        GameManager.Instance.CycleManager.dayCycleChangedEvent -= UpdateDayCycle;
        GameManager.Instance.CycleManager.dayChangedEvent -= UpdateDayCounter;
    }

    private void UpdateDayCounter(int currentDay)
    {
        _currentDayCounter.text = "" + currentDay;
    }

    private void UpdateDayCycle(DayCycle currentDayCycle)
    {
        _currentDayCycle.text = currentDayCycle.ToString();
    }
}
