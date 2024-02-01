using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMineInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _inventoryFullText;

    private Action _clearInventoryAction;
    private Action _informationAction;
    private Action _sellResourcesAction;



    public void SetUIActions(Action clearInventory, Action information, Action sellResources)
    {
        _clearInventoryAction = clearInventory;
        _informationAction = information;
        _sellResourcesAction = sellResources;
    }

    public void SubscribeToInventoryChange(MineInventory mineInventory)
    {
        mineInventory.InventoryChange += UpdateSlider;
    }
    
    public void SellResourcesButtonCall()
    {
        if (_sellResourcesAction != null)
        {
            _sellResourcesAction.Invoke();
        }
    }

    public void InformationButtonCall()
    {
        if (_informationAction != null)
        {
            _informationAction.Invoke();
        }
    }

    public void AddToInventoryButtonCall()
    {
        if (_clearInventoryAction != null)
        {
            _clearInventoryAction.Invoke();
        }
    }

    public void HideButtonCall()
    {
        GameManager.Instance.ResetCameraPosition();
        InventoryCanvas(false);
    }

    public void InventoryCanvas(bool enable)
    {
        _inventoryPanel.SetActive(enable);
    }

    private void UpdateSlider(float percentage)
    {
        _slider.value = percentage;
        if (percentage >= 1)
        {
            _inventoryFullText.gameObject.SetActive(true);
            _slider.value = 1;
        }
        else
        {
            _inventoryFullText.gameObject.SetActive(false);
            _slider.value = percentage;
        }
    }
}
