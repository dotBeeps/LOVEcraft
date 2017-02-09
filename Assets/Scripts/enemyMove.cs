using UnityEngine;
using System.Collections;

public class enemyMove : MonoBehaviour {
    GameObject player;
    public float speed;
    public new Rigidbody2D body;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 2.5f;

    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate() {

        Vector3 fixedTarget = player.transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, fixedTarget - transform.position);
        Vector2 v = fixedTarget - gameObject.transform.position;
        v.Normalize();
        if (Vector3.Distance(gameObject.transform.position, fixedTarget) > 1)
        {
            body.velocity = v * speed;
        }
        else
        {
            body.velocity = new Vector3(0,0,0);
        }


        }
	}

