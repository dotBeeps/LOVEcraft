using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

public class DialogueLoader : MonoBehaviour {

    private TextAsset loadedXmlAsset;
    private bool sendingDialogue = false;
    private bool tickerReady = true;
    private int currentId = 1;

    private List<DialogueNode> dialogueList = new List<DialogueNode>();

    [System.Serializable]
    public struct DialogueNode
    {
        public enum DiagType { MESSAGE,QUESTION,RESPONSE,OTHER};
        public DiagType type { get; set; }
        public string displayText { get; set; }
        public int id { get; set; }
        public int gotoId { get; set; }
        public ArrayList responses { get; set; }
    }

    void LoadXML(string xmlName)
    {
        loadedXmlAsset = Resources.Load("dialogue/" + xmlName) as TextAsset;
        XmlReader reader = new XmlTextReader(new StringReader(loadedXmlAsset.text));
        while (reader.Read())
        {
            DialogueNode currentNode = new DialogueNode();
            if (reader.Name.Equals("m"))
                currentNode.type = DialogueNode.DiagType.MESSAGE;
            else if (reader.Name.Equals("q"))
                currentNode.type = DialogueNode.DiagType.QUESTION;
            else if (reader.Name.Equals("r"))
                currentNode.type = DialogueNode.DiagType.RESPONSE;
            else
                currentNode.type = DialogueNode.DiagType.OTHER;
            if (reader.Name.Equals("q"))
            {
                currentNode.id = int.Parse(reader.GetAttribute("id"));
                reader.ReadToDescendant("m");
                currentNode.displayText = reader.ReadElementContentAsString();
                currentNode.responses = new ArrayList();
                while (reader.ReadToNextSibling("r"))
                {
                    if (reader.Name.Equals("r"))
                    { 
                        DialogueNode responseNode = new DialogueNode();
                        responseNode.gotoId = int.Parse(reader.GetAttribute("go"));
                        responseNode.displayText = reader.ReadElementContentAsString();
                        responseNode.type = DialogueNode.DiagType.RESPONSE;
                        currentNode.responses.Add(responseNode);
                    }
                }
            } else if (reader.Name.Equals("m"))
            {
                currentNode.id = int.Parse(reader.GetAttribute("id"));
                if (reader.AttributeCount>1)
                    currentNode.gotoId = int.Parse(reader.GetAttribute("go"));
                else
                    currentNode.gotoId = currentNode.id + 1;
                currentNode.displayText = reader.ReadElementContentAsString();
                currentNode.type = DialogueNode.DiagType.MESSAGE;
            }
            if (currentNode.type != DialogueNode.DiagType.OTHER)
                dialogueList.Add(currentNode);
        }
    }

    public void UpdateTicker(bool ready)
    {
        tickerReady = ready;
    }

    public void UpdateNextDialogueId(int id)
    {
        currentId = id;
    }

	// Use this for initialization
	void Start () {
        LoadXML("testdialogue");
        sendingDialogue = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (sendingDialogue && tickerReady)
        {
            if (dialogueList.Exists(e => e.id == currentId))
            {
                DialogueNode messageNode = dialogueList.Where(e => e.id == currentId).FirstOrDefault();

                SendMessage("DisplayText", messageNode.displayText);
                if (messageNode.type == DialogueNode.DiagType.QUESTION)
                {
                    DialogueNode[] responses = Array.ConvertAll(messageNode.responses.ToArray(), item => (DialogueNode)item);
                    SendMessage("DisplayResponse", responses);
                }

                Debug.Log(messageNode.gotoId);

                currentId++;
                if (messageNode.gotoId != 0)
                    currentId = messageNode.gotoId;
            }
        }
	}
}
