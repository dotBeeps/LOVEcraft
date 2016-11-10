using UnityEngine;
using System.Collections;

public class CameraSwap : MonoBehaviour {

    private Vector3 targetTransform = new Vector3(0, 0, -10);

	// Use this for initialization
	void Start () {
	
	}
	
    public void updateTarget(Vector3 target)
    {
        targetTransform = target;
    }

	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, targetTransform, 7.0f * Time.deltaTime);
    }
}
