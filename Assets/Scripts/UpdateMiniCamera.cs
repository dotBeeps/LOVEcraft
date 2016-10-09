using UnityEngine;
using System.Collections;

public class UpdateMiniCamera : MonoBehaviour {

    private Vector3 targetTransform = new Vector3(1000, 1000, -10);
    private Camera c;

	// Use this for initialization
	void Start () {
        c = GetComponent<Camera>();
	}

    public void updateTarget(Vector3 target)
    {
        targetTransform = target;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform, 5.0f * Time.deltaTime);
        if (Input.GetAxis("ExpandMap") != 1)
        {
            c.orthographicSize = Mathf.Lerp(c.orthographicSize, 1.75f, 5.0f * Time.deltaTime);
            float x = Mathf.Lerp(c.rect.x, 0.765f, 5.0f * Time.deltaTime);
            float y = Mathf.Lerp(c.rect.y, 0.73f, 5.0f * Time.deltaTime);
            float w = Mathf.Lerp(c.rect.width, 0.2f, 5.0f * Time.deltaTime);
            float h = Mathf.Lerp(c.rect.height, 0.2f, 5.0f * Time.deltaTime);
            c.rect = new Rect(x, y, w, h);
        } else
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(1000, 1000, -10), 5.0f * Time.deltaTime);
            c.orthographicSize = Mathf.Lerp(c.orthographicSize, 2.65f, 5.0f * Time.deltaTime);
            float x = Mathf.Lerp(c.rect.x, 0.125f, 5.0f * Time.deltaTime);
            float y = Mathf.Lerp(c.rect.y, 0.125f, 5.0f * Time.deltaTime);
            float w = Mathf.Lerp(c.rect.width, 0.75f, 5.0f * Time.deltaTime);
            float h = Mathf.Lerp(c.rect.height, 0.75f, 5.0f * Time.deltaTime);
            c.rect = new Rect(x,y,w,h);
        }
    }
}
