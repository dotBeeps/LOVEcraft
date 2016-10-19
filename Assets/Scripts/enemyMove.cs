using UnityEngine;
using System.Collections;

public class enemyMove : MonoBehaviour {
    GameObject player;
    float MoveSpeed = 2.5f;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        Vector3 fixedTarget = player.transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, fixedTarget- transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, fixedTarget, MoveSpeed * Time.deltaTime);
        }
        else
        {
            return;
        }

	}
}
