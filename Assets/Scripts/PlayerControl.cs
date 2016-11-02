using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    [SerializeField]
    private GameObject[] JimNorm = new GameObject[6];
    [SerializeField]
    private GameObject[] JimSide = new GameObject[6];

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
    [SerializeField]
    private List<GameObject> rangedInventory = new List<GameObject>();
    [SerializeField]
    private Sprite backHead;
    [SerializeField]
    private Sprite backBody;
    [SerializeField]
    private Sprite frontHead;
    [SerializeField]
    private Sprite frontBody;

    private Vector2 mousePos;
    private Vector3 screenPos;

    private bool facingLeft = true;

    // Use this for initialization
    void Start () {
        attackCooldownTimer = attackCooldown;
        rangedInventory.Add(currentWeapon);
	}

    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
    void FixedUpdate()
    {
        DoMovement();
        DoAttack();
        DoWeaponSwitch();
    }

    private void DoWeaponSwitch()
    {
        for (int i = 0; i < rangedInventory.Count; i++)
        {
            if (Input.GetKey(keyCodes[i]))
            {
                Debug.Log("Key " + i + " is depressed.");
                if (!currentWeapon.name.Equals(rangedInventory[i].name))
                    SwapWeapon(i);
            }
        }
    }

    private void SwapWeapon(int invSlot)
    {
        currentWeapon.SetActive(false);
        //currentWeapon.hideFlags = HideFlags.HideInHierarchy;
        currentWeapon = rangedInventory[invSlot];
        //currentWeapon.hideFlags = HideFlags.None;
        currentWeapon.SetActive(true);
        if (currentWeapon.transform.parent != transform)
            currentWeapon.transform.SetParent(transform);
    }

    public void PickupItem(GameObject item)
    {
        if (item.GetComponent<RangedWeapon>() != null)
        {
            rangedInventory.Add(item);
            item.transform.SetParent(transform);
            item.transform.position = Vector3.zero;
            item.SendMessage("pickedUp");
            item.transform.localPosition = new Vector3(0.56f, 0);
            item.GetComponent<Collider2D>().enabled = false;
            item.SetActive(false);
            Destroy(item.GetComponent<Rigidbody2D>());
            //item.hideFlags = HideFlags.HideInHierarchy;
        }
        //if (item.GetComponent<MeleeWeapon> != null)
        if (item.GetComponent<Item>() != null)
        {

        }

        
    }

    private void DoAttack()
    {
        Vector2 attackDir = new Vector2(Input.GetAxisRaw("FireHorizontal"), Input.GetAxisRaw("FireVertical"));

        if (attackCooldownTimer < attackCooldown)
        {
            attackCooldownTimer += Time.deltaTime;
            return;
        }
        if (attackDir != Vector2.zero)
        {
            currentWeapon.SendMessage("Attack", attackDir);
            attackCooldownTimer = 0f;
        }
    }

    private void DoMovement()
    {
        rigidbody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * walkSpeed, 0.8f), Mathf.Lerp(0, Input.GetAxis("Vertical") * walkSpeed, 0.8f));

        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if (hor != 0 || ver != 0)
            anim.SetBool("Walking", true);
        if (hor == 0 && ver == 0)
            anim.SetBool("Walking", false);

        if (hor != 0 && !anim.GetBool("Sideways"))
        {
            anim.SetBool("Sideways", true);
            foreach (GameObject g in JimNorm)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in JimSide)
            {
                g.SetActive(true);
            }
        }

        if ((hor == 1 && facingLeft) || (hor == -1 && !facingLeft))
        {
            facingLeft = !facingLeft;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }

        if (ver != 0 && anim.GetBool("Sideways"))
        {
            anim.SetBool("Sideways", false);
            foreach (GameObject g in JimNorm)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in JimSide)
            {
                g.SetActive(false);
            }
        }
        if (ver == 1)
        {
            //frontHead = JimNorm[0].GetComponent<SpriteRenderer>().sprite;
            JimNorm[0].GetComponent<SpriteRenderer>().sprite = backHead;
            //frontBody = JimNorm[1].GetComponent<SpriteRenderer>().sprite;
            JimNorm[1].GetComponent<SpriteRenderer>().sprite = backBody;
        }
        if (ver == -1)
        {
            JimNorm[0].GetComponent<SpriteRenderer>().sprite = frontHead;
            JimNorm[1].GetComponent<SpriteRenderer>().sprite = frontBody;
        }
        

    }


	// Update is called once per frame
	void Update () {
	
	}
}
