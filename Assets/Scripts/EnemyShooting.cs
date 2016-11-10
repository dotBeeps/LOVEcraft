using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {
	public GameObject EnemyProjectile; //enemy projectile prefab
	private float delay = 2f;
	private float timer = 1.5f;

	//use this for initialization
	void Start(){
		InvokeRepeating("FireEnemyProjectile", delay, timer);
	}

	//update is called once per frame
	void Update(){
	}

	//function to fire enemy projectile
	void FireEnemyProjectile(){
		//get reference to player
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if(player != null){ //if player isn't dead
			//instantiate enemy projectile
			GameObject projectile = (GameObject)Instantiate(EnemyProjectile);

			//set projectile's initial position
			projectile.transform.position = transform.position;

			//compute projectile's direction towards player
			Vector2 direction = player.transform.position - projectile.transform.position;

			//adjust direction based on player movement
			PlayerControl Movement = player.GetComponent<PlayerControl>();
			direction.x += (Movement.walkSpeed * Input.GetAxis("Horizontal")) / 4;
			direction.y += (Movement.walkSpeed * Input.GetAxis("Vertical")) / 4;

			//set projectile's direction
			projectile.GetComponent<EnemyProjectile>().SetDirection(direction);
		}
	}
}