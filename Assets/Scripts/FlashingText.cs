using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour {

    Text text;
    bool fading = true;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}

    // Update is called once per frame
    void FixedUpdate() {
        float alpha;
        if (fading)
        {
            alpha = Mathf.Lerp(text.color.a, 0f, 2.0f * Time.deltaTime);
        } else
        {
            alpha = Mathf.Lerp(text.color.a, 255f, 2.0f * Time.deltaTime);
        }
        if (text.color.a < 0.5f)
            fading = false;
        if (text.color.a > 254.5f)
            fading = true;

        Debug.Log("current alpha " + text.color.a);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
	}
}
