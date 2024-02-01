
using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// Resources are extracted from Mine if a Machine is setted
/// Resources are setted one on discovery but can evolved (change name and number not color)
/// When a Cycle passes the resources obtained from the mine are moved to mine inventory (if there is space)
/// When a Day Cycle is completed there is a chance of evolving a resource
/// </summary>
public class Mine : MonoBehaviour
{
    [SerializeField] private bool _isUnlocked;
    [SerializeField] private bool _hasMachine;
    [SerializeField] private float _evolveChance;
    [SerializeField] private MineMeshRenderer _mineMeshRenderer;
    [SerializeField] private MineInventory _mineInventory;
    
    private Resource _mineResource;
    private UIMineHandler _uiMineHandler;

    public void InitializeMine(Resource resource, bool isUnlocked, UIMineHandler uiMineHandler)
    {
        _uiMineHandler = uiMineHandler;
        _mineResource = resource;
        _isUnlocked = isUnlocked;
        enabled = true;
        if (isUnlocked)
        {
            UnlockMine();
        }
            
        
    }

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

    private void ResourceChangeChance()
    {
        float randNumber = Random.Range(0, 1f);
        if (randNumber >= _evolveChance)
        {
            EvolveResource();
        }
    }

    private void EvolveResource()
    {
        //TODO
    }

    private void CyclePassed(DayCycle currentDayCycle)
    {
        
    }

    private void DayPassed(int currentDay)
    {
        ResourceChangeChance();
    }

    public void SetResource(Resource resource)
    {
        _mineResource = resource;
    }

    public void UnlockMine()
    {
        _isUnlocked = true;
        _mineMeshRenderer.ChangeMaterial(_mineResource.color);
        _mineInventory.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!_isUnlocked) return;
        if (_hasMachine) return;
        
        /*Debug.LogFormat("MineHandler Null Check: {0}", _uiMineHandler == null);
        Debug.LogFormat("MineInventory Null Check: {0}", _mineInventory == null);*/
            
        if(_uiMineHandler != null)
            _uiMineHandler.OnMenuOpen(_mineResource, transform, () => { _hasMachine = true; }, _mineInventory);
    }

}
