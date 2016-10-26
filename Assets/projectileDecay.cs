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
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }

	void FixedUpdate ()
    {
        hasLived += Time.deltaTime;
        if (hasLived > lifespan)
        {
            Destroy(gameObject);
        }
    }
}
