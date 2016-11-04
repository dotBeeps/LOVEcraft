using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class DialogueLoader : MonoBehaviour {

    private TextAsset loadedXmlAsset;
    [SerializeField]
    public ArrayList dialogueList = new ArrayList();

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
                currentNode.displayText = reader.ReadElementContentAsString();
                currentNode.type = DialogueNode.DiagType.MESSAGE;
                currentNode.gotoId = currentNode.id + 1;
            }
            if (currentNode.type != DialogueNode.DiagType.OTHER)
                dialogueList.Add(currentNode);
        }
    }


	// Use this for initialization
	void Start () {
        LoadXML("testdialogue");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
