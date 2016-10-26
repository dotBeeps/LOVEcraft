using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameManager instance = null;

	// make sure the constructor is private, so it can only be instantiated here
	private GameManager() {
	}

	//buncha variables (hmu with more variables)
	private static int health = 100;
	private static int damage = 10;

	//set & get methods for all those variables
	public void setHealth(int newVar){
		health = newVar;
	}

	public int getHealth(){
		return health;
	}

	public void setDamage(int newVar){
		damage = newVar;
	}

	public int getDamage(){
		return damage;
	}
		
	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//enforces singleton pattern
			Destroy (gameObject);
		}
			
		DontDestroyOnLoad (gameObject);
	}
}