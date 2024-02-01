using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MineInventory : MonoBehaviour
{
    [SerializeField] private UIMineInventory _uiMineInventory;
    [SerializeField] private int _capacity = 20;
    [SerializeField] private Transform cameraPosition;
    private List<ResourceQuantityTuple> _resourceQuantity = new();
    private int _cacheQuantity;

    public event Action<float> InventoryChange; 
    
    
    private void Start()
    {
        _uiMineInventory.SetUIActions(
            clearInventory: MoveResourcesToGameInventory,
            information: InventoryInformation,
            sellResources: SellResources
            );
        _uiMineInventory.InventoryCanvas(false);
        _uiMineInventory.SubscribeToInventoryChange(this);
    }

    //TODO Test
    /// <summary>
    /// Add resource to inventory
    /// </summary>
    /// <param name="resourceId"></param>
    /// <param name="quantity"></param>
    /// <returns>True if max capacity was reached and excess was discarded</returns>
    public void AddResource(int resourceId, int quantity)
    {
        int toAdd = quantity;
        bool newResource = true;
        
        if (_cacheQuantity + quantity > _capacity)
        {
            toAdd = _capacity - _cacheQuantity;
            //Debug.LogWarning("Excess in Inventory");
        }

        for (int i = 0; i < _resourceQuantity.Count; i++)
        {
            ResourceQuantityTuple item = _resourceQuantity[i];

            if (item.ResourceID == resourceId)
            {
                item.Quantity += toAdd;
                _cacheQuantity += toAdd;
                float fillPercentage = (float)_cacheQuantity / _capacity;
                if (InventoryChange != null) InventoryChange(fillPercentage);
                newResource = false;
                break;
            }
        }

        if (newResource)
        {
            _resourceQuantity.Add(new ResourceQuantityTuple(resourceId, toAdd));
            _cacheQuantity += toAdd;
            float fillPercentage = (float)_cacheQuantity / _capacity;
            if (InventoryChange != null) InventoryChange(fillPercentage);
        }
    }

    private void MoveResourcesToGameInventory()
    {
        Inventory inventory = GameManager.Instance.Inventory;
        for (int i = 0; i < _resourceQuantity.Count; i++)
        {
            ResourceQuantityTuple item = _resourceQuantity[i];
            
            inventory.AddResources(item.ResourceID, item.Quantity);
        }
        
        _resourceQuantity.Clear();
        _cacheQuantity = 0;
        
        if (InventoryChange != null) InventoryChange(0);
    }

    private void OnMouseDown()
    {
        _uiMineInventory.InventoryCanvas(true);
        Camera.main.transform.position = cameraPosition.position;
        Camera.main.transform.rotation = cameraPosition.rotation;
    }

   
    private void InventoryInformation()
    {
        for (int i = 0; i < _resourceQuantity.Count; i++)
        {
            Debug.LogFormat("Resource n° ID {0} | Quantity: {1}", _resourceQuantity[i].ResourceID, _resourceQuantity[i].Quantity);
            
        }
    }

    private void SellResources()
    {
        int moneyGenerated = ResourceSeller.Sell(_resourceQuantity);
       
        GameManager.Instance.AddMoney(moneyGenerated);
        
        _resourceQuantity.Clear();
        _cacheQuantity = 0;
        if (InventoryChange != null) InventoryChange(0);
    }
}
