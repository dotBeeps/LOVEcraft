using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float walkSpeed = 3;
    private float maxSpeed;
    private float curSpeed;
    private new Rigidbody2D rigidbody;
    private Animator anim;

    private Vector2 mousePos;
    private Vector3 screenPos;



    // Use this for initialization
    void Start () {
	
	}

    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
    void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,5.0f));

        transform.rotation = Quaternion.LookRotation(Vector3.forward, screenPos - transform.position);

        rigidbody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * walkSpeed, 0.8f), Mathf.Lerp(0, Input.GetAxis("Vertical") * walkSpeed, 0.8f));

        if (Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical")!=0)
        {
            //anim.SetBool("Walking", true);
        } else
        {
            //anim.SetBool("Walking", false);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
