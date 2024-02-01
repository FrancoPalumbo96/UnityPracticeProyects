using UnityEngine;

public struct Resource 
{
    public int id;
    public string name;
    public int number; //Represents the quantity of Resoruces it will generate by Cycle
    public Color color;

    public override string ToString()
    {
        return name + "-" + number;
    }
}
