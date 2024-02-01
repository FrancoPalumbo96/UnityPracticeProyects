using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIPlaceMachinery : MonoBehaviour
{
    [SerializeField] private GameObject _machineryItem;
    
    public void GenerateMachineryItemGrid(Resource resource, Action<Machine> minePlacementCallback)
    {
        DeleteItems();

        GameManager gm = GameManager.Instance;
        
        List<WorkingMachine> workingMachines = gm.Inventory.MachineryInventory.GetMachineByColor(resource.color);
      
        int machineryInInventory = workingMachines.Count;

        for (int i = 0; i < machineryInInventory; i++)
        {
            WorkingMachine workingMachine = workingMachines[i];
            Machine machine = workingMachine.Machine;
            GameObject newItem = Instantiate(_machineryItem, _machineryItem.transform.parent);
            UIMachineryItem uimi = newItem.GetComponent<UIMachineryItem>();

            Action pickAction = () =>
            {
                workingMachine.InUse = true;
                gameObject.SetActive(false);
                minePlacementCallback.Invoke(machine);
            };
            
            uimi.SetMachinery(machine.Name, machine.Workers, i % 2 == 0, pickAction);
            newItem.SetActive(true);
        }
    }

    private void DeleteItems()
    {
        Transform gridTrans = _machineryItem.transform.parent;

        int totalItems = gridTrans.childCount;
        
        for (int i = totalItems - 1; i > 0; i--)
        {
            Destroy(gridTrans.GetChild(i).gameObject);
        }
    }
}
