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
    private SpriteRenderer spRen;
    private float horOld = 0;
    private float verOld = 0;

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
        spRen = GetComponent<SpriteRenderer>();
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
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if (hor != 0 && ver != 0)
        {
            hor = hor * 0.8f;
            ver = ver * 0.8f; 
        }

        rigidbody.velocity = new Vector2(Mathf.Lerp(0, hor * walkSpeed, 0.8f), Mathf.Lerp(0, ver * walkSpeed, 0.8f));

        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");

        if (ver == 0 && hor == 0)
            anim.Stop();

        string toPlay = "";

        if (hor == 1 && horOld != 1)
        {
            toPlay = "JimSide";
            spRen.flipX = true;
        }
        if (hor == -1 && horOld != -1)
        {
            toPlay = "JimSide";
            spRen.flipX = false;
        }
        if (ver == 1 && verOld != 1)
            toPlay = "JimBack";
        if (ver == -1 && verOld != -1)
            toPlay = "JimForward";
        if (ver == 0 && verOld != 0 && hor != 0)
        {
            toPlay = "JimSide";
            spRen.flipX = hor == 1;
        }
        if (hor == 0 && horOld != 0 && ver == 1)
            toPlay = "JimBack";
        if (hor == 0 && horOld != 0 && ver == -1)
            toPlay = "JimForward";

        if (!toPlay.Equals(""))
        {
            Debug.Log("Playing anim: " + toPlay);
            anim.Play(toPlay);
        }

        horOld = hor;
        verOld = ver;
    }


	// Update is called once per frame
	void Update () {
	
	}
}
