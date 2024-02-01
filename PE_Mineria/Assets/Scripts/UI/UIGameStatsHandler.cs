using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameStatsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _toxicityText;

    private void Start()
    {
        GameManager gm = GameManager.Instance;

        gm.MoneyChanged += UpdateMoney;
        gm.ToxicityChanged += UpdateToxicity;
        
    }

    private void UpdateMoney(int money)
    {
        _moneyText.text = "Money: " + money;
    }

    private void UpdateToxicity(int toxicity)
    {
        _toxicityText.text = "Toxicity: " + toxicity;
    }
}
