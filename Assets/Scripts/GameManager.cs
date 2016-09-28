using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameManager instance = null;

	// make sure the constructor is private, so it can only be instantiated here
	private GameManager() {
	}

	//buncha variables (hmu with more variables)
	private int variable;

	//set & get methods for all those variables
	public void setVariable(int newVar){
		variable = newVar;
	}

	public int getVariable(){
		return variable;
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