using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceDictionary
{
    public static Dictionary<int, Resource> ResourcesById = new()
    {
        [0] = new()
        {
            id = 1,
            name = "Aether",
            color = Color.blue
        },
        [1] = new()
        {
            id = 2,
            name = "Bombastium",
            color = Color.red
        },
        [2] = new()
        {
            id = 3,
            name = "Uridium",
            color = Color.green
        },
    };

    public static int NumberOfResources()
    {
        return ResourcesById.Keys.Count;
    }
} 
