using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon {

    [SerializeField]
    private GameObject weaponProjectile;
    [SerializeField]
    private float vel = 1000f;

	public RangedWeapon(string name) : base(name)
    {

    }

    public RangedWeapon(string name, Projectile projectile) : base(name)
    {

    }

    public override void Attack(Vector2 dirAttack)
    {
        dirAttack = dirAttack * vel;
        //Debug.Log(dirAttack.x + " " + dirAttack.y);

        GameObject projectile;
        projectile = Instantiate(weaponProjectile, transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb2d = projectile.GetComponent<Rigidbody2D>();
        rb2d.AddForce(dirAttack * vel, ForceMode2D.Force);
    }
}
