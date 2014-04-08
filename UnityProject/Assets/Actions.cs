using UnityEngine;
using System.Collections;

public class Actions : MonoBehaviour {

	public bool isJumping = false;
	public bool isFacingRight = true;
	private float jumpForce =  300000.0f;
	private float moveForce = 1000000.0f;
	private float maxVelocity = 10.0f;

	private Animator anim;					// Reference to the player's animator component.
	
	
	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

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

				if(isFacingRight) {
					rigidbody2D.AddForce(Time.deltaTime*moveForce * Vector2.right);
				} else {
					//Flip to facing right
					transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				}
				isFacingRight = true;
			} else if (h < 0.0f) {

				//Debug.Log ("Move left");
				if(!isFacingRight) {
					rigidbody2D.AddForce(-Time.deltaTime*moveForce * Vector2.right);
				} else {
					//Flip to facing left
					transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
				}
				isFacingRight = false;
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

		var attackBtnDown = Input.GetButtonDown("Fire1");
		if(attackBtnDown) {
			anim.SetTrigger("Attack");
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
