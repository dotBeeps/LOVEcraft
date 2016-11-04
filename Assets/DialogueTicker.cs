using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueTicker : MonoBehaviour {

    public Text diagText;
    private char[] textBuffer;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

    public void DisplayText(string text)
    {
        textBuffer = text.ToCharArray();
    }
}
