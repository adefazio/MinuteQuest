using UnityEngine;
using System.Collections;

public class Actions : MonoBehaviour {

	public bool isJumping = false;
	public float jumpForce =  300000.0f;
	public float moveForce = 100000.0f;
	public float maxVelocity = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		var vel = rigidbody2D.velocity;
		//Debug.Log ("Velocity: " + vel.magnitude);
		if(vel.magnitude > maxVelocity){
			rigidbody2D.velocity = maxVelocity*vel/vel.magnitude;
		}

		float h = Input.GetAxis("Horizontal");
		
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		if(vel.magnitude < maxVelocity) {
			if (h > 0.0f) {
				//Debug.Log ("Move right");
				rigidbody2D.AddForce(moveForce * Vector2.right);
			} else if (h < 0.0f) {
				//Debug.Log ("Move left");
				rigidbody2D.AddForce(-moveForce * Vector2.right);
			} else {
				rigidbody2D.velocity = Vector2.zero;
			}
		}

		/////////////////////////////////////

		var jumpBtnDown = Input.GetButtonDown("Jump");

		if(jumpBtnDown && !isJumping) {
			Debug.Log("Jump triggered", gameObject);
			isJumping = true;
			rigidbody2D.AddForce(Vector2.up * jumpForce);
		}

		

	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("On Trigger Enter");
	}

	void OnCollisionEnter2D(Collision2D collision) {

		// Debug-draw all contact points and normals
		foreach(ContactPoint2D contact in collision.contacts) {
			//Debug.DrawRay(contact.point, contact.normal, Color.white);
		}

		Debug.Log("Hit Something");
		
		if(collision.gameObject.tag == "floor") {
			isJumping = false;
			Debug.Log("Hit floor");
		}
	}
}
