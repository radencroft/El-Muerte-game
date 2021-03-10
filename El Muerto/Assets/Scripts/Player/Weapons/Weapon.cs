using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public int id;
    public string name;
    public bool unlocked; 
    public int bullets; 

    public Weapon(int id, bool unlocked, string name, int bullets)     
    {
        this.id = id;
        this.unlocked = unlocked;
        this.name = name; 
        this.bullets = bullets; 
    }
}
