using UnityEngine;
using System.Collections;

public class Actions : MonoBehaviour {

	public bool isJumping = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		if (h > 0.0f) {
						rigidbody2D.velocity = new Vector2 (10.0f, 0.0f);
				} else if (h < 0.0f) {
						rigidbody2D.velocity = new Vector2 (-10.0f, 0.0f);
				} else {
						rigidbody2D.velocity = new Vector2 (0.0f, 0.0f);
				}

		var jumpBtnDown = Input.GetButtonDown ("Jump");

		if(jumpBtnDown && !isJumping) {
			isJumping = true;
		}
	}
}
