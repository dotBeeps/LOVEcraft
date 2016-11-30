using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	private static GameManager gameManager;
	public static GameObject GameController;
	
	public int startingHealth;                                  // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


	Animator anim;                                              // Reference to the Animator component.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerControl enemyMovement;                                    // Reference to the player's movement.
	EnemyShooting enemyShooting;                               // Reference to the PlayerShooting script.
	public bool isDead;                                         // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.


	void Awake (){
		// Setting up the references.
		//anim = GetComponent <Animator> ();
		//enemyAudio = GetComponent <AudioSource> ();
		//enemyMovement = GetComponent <Movement> ();
		enemyShooting = GetComponentInChildren <EnemyShooting> ();

		// Set the initial health of the enemy.
		currentHealth = startingHealth;
	}


	void Update (){
		// If the enemy has just been damaged...
		if(damaged){
			// ... set the colour of the damageImage to the flash colour.
			//damageImage.color = flashColour;
		}
		// Otherwise...
		else{
			// ... transition the colour back to clear.
			//damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}

	void OnCollisionEnter2D (Collision2D col){
		GameController = GameObject.Find("GameController");
		gameManager = GameController.GetComponent<GameManager>();
        int damage = 10;
		if(col.transform.tag.Equals("Projectile")){	//change this to use tags! //I gotchu fam -David
			damaged = true;
			TakeDamage(damage);
		}
	}

	public void TakeDamage (int amount){
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Set the health bar's value to the current health.
		//healthSlider.value = currentHealth;

		// Play the hurt sound effect.
		//playerAudio.Play ();

		// If the player has lost all its health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead){
			// ... it should die.
			Death ();
		}
	}


	void Death (){
		// Set the death flag so this function won't be called again.
		isDead = true;

		// Turn off any remaining shooting effects.
		//PlayerShooting.DisableEffects ();

		// Tell the animator that the player is dead.
		//anim.SetTrigger ("Die");

		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();

		// Destroy enemy
		Destroy(gameObject);
	}       
}
