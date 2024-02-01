using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MachineryInventory
{
    private List<WorkingMachine> _inventoryMachines = new()
    {
        new WorkingMachine(1, new Machine(0, "Oldy", Color.blue, 0, 0, 0.5f), false)
    };

    public List<WorkingMachine> Inventory => _inventoryMachines;

    private static int WorkingMachineIDHandler = 1;
    
    public void PurchaseMachine(Machine newMachine)
    {
        WorkingMachine workingMachine = new WorkingMachine(WorkingMachineIDHandler, newMachine, false);
        WorkingMachineIDHandler += 1;
        _inventoryMachines.Add(workingMachine);
    }

    /// <summary>
    /// Get All Owned Machines
    /// </summary>
    /// <param name="color">Machine Color</param>
    /// <returns></returns>
    public List<WorkingMachine> GetMachineByColor(Color color)
    {
        List<WorkingMachine> machines = new();

        foreach (WorkingMachine workingMachine in _inventoryMachines)
        {
            Machine machine = workingMachine.Machine;
            if (machine.ResourceColor != color) continue;
            if (workingMachine.InUse) continue;
            machines.Add(workingMachine);
        }

        return machines;
    }
    
    public void ChangeMachineToInUseById(int workingMachineID)
    {
        foreach (WorkingMachine workingMachine in _inventoryMachines)
        {
            if (workingMachine.ID == workingMachineID)
                workingMachine.InUse = true;
        }
    }
    
}

public class WorkingMachine
{
    public readonly int ID;
    public readonly Machine Machine;
    public bool InUse;

    public WorkingMachine(int id, Machine machine, bool inUse)
    {
        ID = id;
        Machine = machine;
        InUse = inUse;
    }

    public override string ToString()
    {
        return Machine.ToString();
    }
}
public class Machine
{
    public readonly int ID;
    public readonly string Name;
    public readonly Color ResourceColor;
    public readonly int Cost;
    public readonly int Workers;
    public readonly float WorkForce;
    public bool InStock;

    public Machine(int id, string name, Color resourceColor, int cost, int workers, float workForce)
    {
        ID = id;
        Name = name;
        ResourceColor = resourceColor;
        Cost = cost;
        Workers = workers;
        WorkForce = workForce;
        InStock = true;
    }

    public override string ToString()
    {
        return String.Format("[{0} | '{1}' | {2}| ${3} | {4} men | {5}x]", 
            ID, Name, ColorToString.ColorName(ResourceColor), Cost, Workers, WorkForce);
    }
}

