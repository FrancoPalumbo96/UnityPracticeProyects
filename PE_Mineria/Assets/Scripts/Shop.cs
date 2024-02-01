using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    private List<Machine> _shopMachines = new List<Machine>
    {
        new (1, "Breaker X", Color.blue, 100, 1, 1.0f),
        new (2, "ExoDrill", Color.red, 100, 1, 1.0f),
        new (3, "Graffiti 33", Color.green, 100, 1, 1.0f),
        new (4, "Graviton Gusher", Color.blue, 300, 2, 1.5f),
        new (5, "Fusion Flux Driller", Color.red, 350, 3, 1.6f),
        new (5, "Quantum Pulse Excavator", Color.green, 350, 2, 1.55f),
    };

    public List<Machine> MachinesInShop => _shopMachines;
    
    
    public (bool purchased, string failReason) PurchaseMachine(int id)
    {
        foreach (Machine machine in _shopMachines)
        {
            if (machine.ID == id)
            {
                bool purchaseDone = GameManager.Instance.Purchase(cost: machine.Cost);
                if (purchaseDone)
                {
                    machine.InStock = false;
                    return (true, "none");
                }
                
                return (false, "Not enough funds!");
            }
                
        }

        return (false, "Machine not found!");
    }
}
