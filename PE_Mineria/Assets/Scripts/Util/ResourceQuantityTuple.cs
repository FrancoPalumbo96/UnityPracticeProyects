using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceQuantityTuple 
{
    public int ResourceID;
    public int Quantity;

    public ResourceQuantityTuple(int resourceID, int quantity)
    {
        ResourceID = resourceID;
        Quantity = quantity;
    }

    public override string ToString()
    {
        Resource resource = ResourceDictionary.ResourcesById[ResourceID];
        return String.Format("Resource: {0} | Quantity: {1}", ((ValueType)resource).ToString(), Quantity);
    }
}
