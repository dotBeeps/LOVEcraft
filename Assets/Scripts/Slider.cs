using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slider : MonoBehaviour {
	public UnityEngine.UI.Slider healthSlider;
	private static GameManager gameManager;
	public static GameObject GameController;

	// Use this for initialization
	void Start () {
		// get the max health of the player.
		GameController = GameObject.Find("GameController");
		gameManager = GameController.GetComponent<GameManager>();
		float maxHealth = gameManager.getHealth();

		healthSlider.maxValue = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
