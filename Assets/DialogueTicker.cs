using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueTicker : MonoBehaviour {

    public Text diagText;
    private Queue textBuffer = new Queue();
    public float speed = .1f;
    public float fastSpeed = .05f;
    private float delay = 0f;
    [SerializeField]
    private bool buttonsActive = false;
    private List<GameObject> buttonsToActivate = new List<GameObject>();

    public GameObject response1;
    public GameObject response2;
    public GameObject response3;
    public GameObject response4;

    private GameObject[] buttons;
    private int[] ids = new int[4];

	// Use this for initialization
	void Start () {
        buttons = new GameObject[4];
        buttons[0] = response1;
        buttons[1] = response2;
        buttons[2] = response3;
        buttons[3] = response4;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Don't try and output things if the textbuffer is empty.
        if (textBuffer.Count == 0)
        {
            if (buttonsToActivate.Count > 0)
            {
                foreach (GameObject b in buttonsToActivate)
                {
                    b.SetActive(true);
                }
                buttonsToActivate.Clear();
            }
            if (Input.GetAxisRaw("AdvanceDialogue")==1 && !buttonsActive)
            {
                SendMessage("UpdateTicker", true);
            }
            return;
        }
        // Set up holding down a button to speed up dialogue.
        float maxCount = speed;
        if (Input.GetAxisRaw("FastDialogue") == 1)
        {
            maxCount = fastSpeed;
        }

        // Check if enough time has passed since last letter has been output, then add to text if it has been.
        if (delay > maxCount) 
        {
            diagText.text = diagText.text + textBuffer.Dequeue();
            delay = 0f;
        }
        delay = delay + Time.deltaTime;
	}

    public void DisplayText(string text)
    {
        diagText.text = "";
        char[] textArr = text.ToCharArray();
        for (int i = 0; i < textArr.Length; i++)
        {
            textBuffer.Enqueue(textArr[i]);
        }
        SendMessage("UpdateTicker", false);
    }

    public void DisplayResponse(DialogueLoader.DialogueNode[] responses)
    {
        for (int i = 0; i < responses.Length; i++)
        {
            ids[i] = responses[i].gotoId;
            buttons[i].GetComponentInChildren<Text>().text = responses[i].displayText;
            buttonsToActivate.Add(buttons[i]);
            buttonsActive = true;
        }
    }

    private void DisableButtons()
    {
        buttonsActive = false;
        foreach (GameObject b in buttons)
        {
            b.SetActive(false);
        }
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            
    }

    public void Button1Response()
    {
        SendMessage("UpdateTicker", true);
        SendMessage("UpdateNextDialogueId",ids[0]);
        DisableButtons();
    }

    public void Button2Response()
    {
        SendMessage("UpdateTicker", true);
        SendMessage("UpdateNextDialogueId", ids[1]);
        DisableButtons();
    }

    public void Button3Response()
    {
        SendMessage("UpdateTicker", true);
        SendMessage("UpdateNextDialogueId", ids[2]);
        DisableButtons();
    }

    public void Button4Response()
    {
        SendMessage("UpdateTicker", true);
        SendMessage("UpdateNextDialogueId", ids[3]);
        DisableButtons();
    }
}
