﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour{
	private static GameManager gameManager;
	public static GameObject GameController;

	public int startingHealth;                                  // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public UnityEngine.UI.Slider healthSlider;                  // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(255, 255, 255, 1);     // The colour the damageImage is set to, to flash.


	Animator anim;                                              // Reference to the Animator component.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	Movement playerMovement;                                    // Reference to the player's movement.
	//PlayerShooting playerShooting;                            // Reference to the PlayerShooting script.
	public bool isDead;                                         // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.


	void Awake (){
		// Setting up the references.
		//anim = GetComponent <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <Movement> ();
		//playerShooting = GetComponentInChildren <PlayerShooting> ();

		// Set the initial health of the player.
		GameController = GameObject.Find("GameController");
		gameManager = GameController.GetComponent<GameManager>();
		startingHealth = gameManager.getHealth();
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;
	}


	void Update (){
		// If the player has just been damaged...
		if(damaged){
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}

	void OnCollisionEnter2D (Collision2D col){
		//Check collision name
		Debug.Log("collision name = " + col.gameObject.name);
		if(col.gameObject.name == "EnemyProjectile" || col.gameObject.name == "EnemyProjectile(Clone)"){
			damaged = true;
			TakeDamage(50);
		}
	}

	public void TakeDamage (int amount){
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;

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

		// Turn off the movement and shooting scripts.
		playerMovement.enabled = false;
		//playerShooting.enabled = false;
	}       
}