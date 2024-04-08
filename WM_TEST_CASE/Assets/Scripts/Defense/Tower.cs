using System;
using UnityEngine;

[Serializable] // Emphasize that a class or a struct can be serialized.
public class Tower
{
    // Tower attributes
    public string name;         
    public int cost;            
    public GameObject prefab;  

    // Constructor for Tower class
    public Tower(string _name, int _cost, GameObject _prefab)
    {
        // Assigning values to fields
        name = _name;           
        cost = _cost;           
        prefab = _prefab;      
    }
}

