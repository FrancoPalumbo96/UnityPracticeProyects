using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static class ResourcesFactory
{
    /// <summary>
    /// Returns random resource
    /// </summary>
    /// <returns></returns>
    public static Resource GetResource()
    {
        int nOfResources = ResourceDictionary.NumberOfResources();

        int newResourceId = Random.Range(0, nOfResources);

        return GetResource(newResourceId);
    }

    public static Resource GetResource(int resourceID)
    {
        Resource dict = ResourceDictionary.ResourcesById[resourceID];

        int currentDay = GameManager.Instance.CycleManager.DayCounter;
        int aux = currentDay / 7;
        int resourceNumber = (aux + 1) * 10;
        
        
        return new Resource
        {
            id = resourceID,
            name = dict.name,
            number = resourceNumber,
            color = dict.color
        };
    }
}
