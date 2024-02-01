using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMiningMachine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _machineSummary;

    public void UpdateMachineText(Machine machine, string resource, int production)
    {
        _machineSummary.text = String.Format(
            "Name: {0} \n" + 
            "Workers: {1} \n" +
            "Work Force: {2} \n" +
            "Cost: {3} \n" +
            "Resource: {4} \n" +
            "Production: {5}", machine.Name, machine.Workers, machine.WorkForce, machine.Cost, resource, production);
    }
}
