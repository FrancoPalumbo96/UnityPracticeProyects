using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMachineryItem : MonoBehaviour
{
    [SerializeField] private Image _itemBackground;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _workers;
    [SerializeField] private Button _chooseMachineryButton;

    private Action _actionOnPick;

    public void SetMachinery(string machineryName, int workers, bool oddItem, Action onButtonClick)
    {
        _name.text = machineryName;
        _workers.text = "" + workers;
        _itemBackground.color = oddItem ? Color.gray : Color.white;
        _actionOnPick = onButtonClick;
        _chooseMachineryButton.onClick.AddListener(this.OnMachineryPicked);
    }

    private void OnMachineryPicked()
    {
        _actionOnPick.Invoke();
    }
    
}
