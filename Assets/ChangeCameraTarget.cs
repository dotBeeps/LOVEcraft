using UnityEngine;
using System.Collections;

public class ChangeCameraTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, -10);
            GameObject.FindGameObjectWithTag("MainCamera").SendMessage("updateTarget", pos);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
