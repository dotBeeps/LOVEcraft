using UnityEngine;
using System.Collections;

public class projectileDecay : MonoBehaviour {

    [SerializeField]
    private float lifespan = 1.2f;

    private float hasLived = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Projectile died by collision.");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Projectile died by trigger with tag: " + col.tag + " With gameobject name: " + col.name);
        Destroy(gameObject);
    }

	void FixedUpdate ()
    {
        hasLived += Time.deltaTime;
        if (hasLived > lifespan)
        {
            Debug.Log("Projectile died of old age.");
            Destroy(gameObject);
        }
    }
}
