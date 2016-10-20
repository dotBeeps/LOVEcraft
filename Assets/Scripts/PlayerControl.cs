using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float walkSpeed = 3;
    private float maxSpeed;
    private float curSpeed;
    private new Rigidbody2D rigidbody;
    private Animator anim;

    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private float attackCooldownTimer;

    private Vector2 lookTarget;

    [SerializeField]
    private GameObject currentWeapon;

    private Vector2 mousePos;
    private Vector3 screenPos;



    // Use this for initialization
    void Start () {
        attackCooldownTimer = attackCooldown;
	}

    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
    void FixedUpdate()
    {
        DoMovement();

        Vector2 attackDir = new Vector2(Input.GetAxisRaw("FireHorizontal"), Input.GetAxisRaw("FireVertical"));

        if (attackCooldownTimer < attackCooldown)
        {
            attackCooldownTimer += Time.deltaTime;
            return;
        }
        if (attackDir != Vector2.zero)
        {
            currentWeapon.SendMessage("Attack",attackDir);
            attackCooldownTimer = 0f;
        }
    }

    private void DoMovement()
    {
        rigidbody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * walkSpeed, 0.8f), Mathf.Lerp(0, Input.GetAxis("Vertical") * walkSpeed, 0.8f));

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            lookTarget = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("FireHorizontal") != 0 || Input.GetAxisRaw("FireVertical") != 0)
            lookTarget = new Vector2(Input.GetAxisRaw("FireHorizontal"), Input.GetAxisRaw("FireVertical"));

        float angle = Mathf.Atan2(lookTarget.y, lookTarget.x) * Mathf.Rad2Deg - 90;

        float lerpAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, angle, 10.0f * Time.deltaTime);

        transform.rotation = Quaternion.AngleAxis(lerpAngle, Vector3.forward);
        
    }


	// Update is called once per frame
	void Update () {
	
	}
}
