using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	float speed; //the bullet speed
	Vector2 _direction; //the direction of the bullet
	bool isReady; //whether the bullet direction is set

	//set default value in Awake function
	void Awake(){
		speed = 5f;
		isReady = false;
	}

	// Use this for initialization
	void Start () {
	
	}

	public void SetDirection(Vector2 direction){
		//set the direction normalized to get a unit vector
		_direction = direction.normalized;

		isReady = true; //set flag to true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isReady) {
			//get the bullet's current position
			Vector2 position = transform.position;

			//compute the bullet's new position
			position += _direction * speed * Time.deltaTime;

			//update the bullet's position;
			transform.position = position;

			//remove bullet if it goes outside the screen
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0)); //bottom left
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1)); //top right
			if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
			    (transform.position.y < min.y) || (transform.position.y > max.y)) {
				Destroy (gameObject);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col){
<<<<<<< HEAD
		if(col.gameObject.name != "enemy"){
=======
		//Check collision name
		Debug.Log("collision name = " + col.gameObject.name);
		if(!col.transform.tag.Contains("Enemy") && !col.transform.tag.Contains("Projectile")){
>>>>>>> refs/remotes/origin/master
			Destroy(gameObject);
		}
	}
}
