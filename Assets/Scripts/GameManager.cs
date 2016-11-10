using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	// make sure the constructor is private, so it can only be instantiated here
	private GameManager() {
	}
		
	//buncha variables (hmu with more variables)
	private static float health = 100;
	private static float damageMultiplier = 1.0f;
	private static float movementMultiplier = 1.0f;
	private static float projectileMultiplier = 1.0f;
	private static float fireRateMultiplier = 1.0f;
	private static float charisma = 1.0f;

	//set & get methods for all those variables

	//Health
	public void setHealth(int newVar){
		health = newVar;
	}
	public void updateHealth(float addedVar){
		health += addedVar;
	}
	public float getHealth(){
		return health;
	}


	//Damage Multiplier
	public void setDamageMultiplier(float newVar){
		damageMultiplier = newVar;
	}
	public void updateDamageMultiplier(float addedVar){
		damageMultiplier += addedVar;
	}
	public float getDamageMultiplier(){
		return damageMultiplier;
	}


	//Charisma
	public void setCharisma(float newVar){
		charisma = newVar;
	}
	public void updateCharisma(float addedVar){
		charisma += addedVar;
	}
	public float getCharisma(){
		return charisma;
	}


	//Movement Multiplier
	public void setMovementMultiplier(float newVar){
		movementMultiplier = newVar;
	}
	public void updateMovementMultiplier(float addedVar){
		movementMultiplier += addedVar;
	}
	public float getMovementMultiplier(){
		return movementMultiplier;
	}


	//Projectile Multiplier
	public void setProjectileMultiplier(float newVar){
		projectileMultiplier = newVar;
	}
	public void updateProjectileMultiplier(float addedVar){
		projectileMultiplier += addedVar;
	}
	public float getProjectileMultiplier(){
		return projectileMultiplier;
	}


	//Fire Rate Multiplier
	public void setFireRateMultiplier(float newVar){
		fireRateMultiplier = newVar;
	}
	public void updateFireRateMultiplier(float addedVar){
		fireRateMultiplier += addedVar;
	}
	public float getFireRateMultiplier(){
		return fireRateMultiplier;
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