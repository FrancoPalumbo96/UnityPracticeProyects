using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorToString
{
    public static string ColorName(Color color)
    {
        if (color == Color.blue)
            return "blue";
        if (color == Color.green)
            return "green";
        if (color == Color.red)
            return "red";
        
        return "not found";
    }
}
