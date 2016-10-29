using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class Weapon : Item {

    private string weaponName;

    public Weapon()
    {

    }

    public Weapon(string name)
    {
    }

    public Weapon setDisplayName(string name)
    {
        weaponName = name;
        return this;
    }

    public string getNameUnlocalized()
    {
        return weaponName; 
    }

    public abstract void Attack(Vector2 dirAttack);
	
}
