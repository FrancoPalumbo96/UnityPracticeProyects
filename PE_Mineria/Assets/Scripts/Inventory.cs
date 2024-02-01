using System.Collections.Generic;

public class Inventory 
{
    private List<ResourceQuantityTuple> _resourceQuantity = new();
    private MachineryInventory _machineryInventory = new();
    
    public MachineryInventory MachineryInventory => _machineryInventory;

    public void AddResources(int resourceID, int quantity)
    {
        bool newResource = true;
        
        for (int i = 0; i < _resourceQuantity.Count; i++)
        {
            ResourceQuantityTuple item = _resourceQuantity[i];

            if (item.ResourceID == resourceID)
            {
                item.Quantity += quantity;
                newResource = false;
                break;
            }
        }

        if (newResource)
        {
            _resourceQuantity.Add(new ResourceQuantityTuple(resourceID, quantity));
        }
    }

    public List<ResourceQuantityTuple> ResourceInventory => _resourceQuantity;
}
