using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon {

    private GameObject weaponProjectile;

	public RangedWeapon(int id, string name) : base(id, name)
    {
    }

    public RangedWeapon(int id, string name, Projectile projectile) : base(id, name)
    {

    }

    public RangedWeapon(int id) : base(id)
    {
        
    }

    public override void Attack()
    {

    }
}
