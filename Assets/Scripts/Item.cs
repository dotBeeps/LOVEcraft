using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    private Transform player;
    private Rigidbody2D rigid;
    public bool inInv = false;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float maxMoveSpeed = 7f;

	public Item()
    {

    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        if (moveSpeed == 0)
            moveSpeed = 2f;
        if (maxMoveSpeed == 0)
            maxMoveSpeed = 7f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!inInv &&col.tag.Equals("Player"))
            col.SendMessage("PickupItem", gameObject);
    }

    void FixedUpdate()
    {
        if (!inInv && Vector2.Distance(transform.position, player.position) < 4f)
        {
            //Debug.Log("pushing item towards player");
            Vector2 dir = player.position - transform.position;
            dir.Normalize();
            
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            moveSpeed = Mathf.Lerp(moveSpeed, maxMoveSpeed, 4f * Time.deltaTime);
            //rigid.AddForce(dir * 1f, ForceMode2D.Impulse);
        }
    }
}
