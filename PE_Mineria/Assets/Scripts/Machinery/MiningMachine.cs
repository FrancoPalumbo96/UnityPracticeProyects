using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningMachine : MonoBehaviour
{
    [SerializeField] private UIMiningMachine _uiMiningMachine;
    private Machine _machine;
    private Resource _resource;
    private MineInventory _mineInventory;
    
    private void OnEnable()
    {
        GameManager.Instance.CycleManager.dayCycleChangedEvent += CyclePassed;
        GameManager.Instance.CycleManager.dayChangedEvent += DayPassed;
    }

    private void OnDisable()
    {
        GameManager.Instance.CycleManager.dayCycleChangedEvent -= CyclePassed;
        GameManager.Instance.CycleManager.dayChangedEvent -= DayPassed;
    }

    private int CycleProduction()
    {
        return Mathf.RoundToInt(_resource.number * _machine.WorkForce);
    }

    private void SetMachineUI()
    {
        _uiMiningMachine.UpdateMachineText(_machine, _resource.ToString(), CycleProduction());
    }

    private void CyclePassed(DayCycle currentDayCycle)
    {
        _mineInventory.AddResource(_resource.id, CycleProduction());
    }

    private void DayPassed(int currentDay)
    {
        //Debug.Log("Reduce Cost of Operating");
        GameManager.Instance.AddToxicity(5);
    }

    public void SetMiningMachine(Machine machine, Resource resource, MineInventory mineInventory)
    {
        _machine = machine;
        _resource = resource;
        _mineInventory = mineInventory;
        SetMachineUI();
    }
}
