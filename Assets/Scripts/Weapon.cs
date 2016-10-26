using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponInitializer
{
    Mixtape mixtape = new Mixtape(0, "weaponRangedMixtape");


}


public abstract class Weapon : Item {

    static Dictionary<int, Weapon> weaponDatabase = new Dictionary<int, Weapon>();

    private string weaponName;
    private int weaponId;

    public Weapon()
    {

    }

    public Weapon(int id, string name)
    {
        setId(id).setNameUnlocalized(name);
    }

    public Weapon(int id)
    {
        setId(id);
    }

    public Weapon setNameUnlocalized(string name)
    {
        weaponName = name;
        return this;
    }

    public string getNameUnlocalized()
    {
        return weaponName; 
    }

    public int getId()
    {
        return weaponId;
    }

    public Weapon setId(int id)
    {
        int oldId = weaponId;
        weaponId = id;

        weaponDatabase.Remove(oldId);
        weaponDatabase.Add(weaponId, this);

        return this;
    }

    public abstract void Attack();
	
}
