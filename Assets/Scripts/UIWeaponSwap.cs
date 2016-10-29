using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class UIWeaponSwap : MonoBehaviour {
	GameObject[] gameObjectArray;


	// Use this for initialization
	void Start (){
		gameObjectArray = GameObject.FindGameObjectsWithTag ("WeaponImage");
		activate (9);
	}
	
	// Update is called once per frame
	void Update (){
		if (Input.GetKey (KeyCode.Alpha1)) {
			activate (9);
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			activate (8);
		} else if (Input.GetKey (KeyCode.Alpha3)) {
			activate (7);
		} else if (Input.GetKey (KeyCode.Alpha4)) {
			activate (6);
		} else if (Input.GetKey (KeyCode.Alpha5)) {
			activate (5);
		} else if (Input.GetKey (KeyCode.Alpha6)) {
			activate (4);
		} else if (Input.GetKey (KeyCode.Alpha7)) {
			activate (3);
		} else if (Input.GetKey (KeyCode.Alpha8)) {
			activate (2);
		} else if (Input.GetKey (KeyCode.Alpha9)) {
			activate (1);
		} else if (Input.GetKey (KeyCode.Alpha0)) {
			activate (0);
		}
	
	}

	void deactivate(){
		Debug.Log (gameObjectArray);
		foreach(GameObject go in gameObjectArray)
		{
			go.SetActive (false);
		}
	}

	void activate(int i){
		deactivate ();
		gameObjectArray[i].SetActive(true);
	}
}
