using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceSeller
{
    private static Dictionary<int, int> ResourceSellValueByID = new Dictionary<int, int>
    {
        [0] = 5,
        [1] = 7,
        [2] = 8,
    };
    
    
    public static int Sell(List<ResourceQuantityTuple> resourcesList)
    {
        int sellAmount = 0;

        foreach (ResourceQuantityTuple resourceQuantity in resourcesList)
        {
            int sellValue = ResourceSellValueByID[resourceQuantity.ResourceID];
            
            sellAmount += sellValue * resourceQuantity.Quantity;
        }
        
        return sellAmount;
    }
    
    
}
