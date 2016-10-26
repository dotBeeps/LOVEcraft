using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    private Transform player;
    private Rigidbody2D rigid;
    public bool inInv = false;

	public Item()
    {

    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!inInv && Vector2.Distance(transform.position, player.position) < 4f)
        {
            Debug.Log("pushing item towards player");
            Vector2 dir = player.position - transform.position;
            dir.Normalize();
            rigid.AddForce(dir * 15f);
        }
    }
}
