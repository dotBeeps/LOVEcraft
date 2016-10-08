using UnityEngine;
using System.Collections;

public class UpdateMiniCamera : MonoBehaviour {

    private Vector3 targetTransform = new Vector3(1000, 1000, -10);

	// Use this for initialization
	void Start () {
	
	}

    public void updateTarget(Vector3 target)
    {
        targetTransform = target;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform, 5.0f * Time.deltaTime);
    }
}
