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

    private Vector2 mousePos;
    private Vector3 screenPos;



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
                Debug.Log("Key " + i+1 + " is depressed.");
                if (!currentWeapon.name.Equals(rangedInventory[i].name))
                    SwapWeapon(i);
            }
        }
    }

    private void SwapWeapon(int invSlot)
    {
        currentWeapon.SetActive(false);
        currentWeapon.hideFlags = HideFlags.HideInHierarchy;
        currentWeapon = rangedInventory[invSlot];
        currentWeapon.hideFlags = HideFlags.None;
        currentWeapon.SetActive(true);
        currentWeapon.transform.SetParent(transform);
    }

    public void PickupItem(GameObject item)
    {
        if (item.GetComponent<RangedWeapon>() != null)
        {
            rangedInventory.Add(item);
            item.transform.SetParent(transform);
            item.transform.position = Vector3.zero;
            item.GetComponent<Collider2D>().enabled = false;
            item.SetActive(false);
            item.hideFlags = HideFlags.HideInHierarchy;
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
