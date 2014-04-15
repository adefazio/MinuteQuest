using UnityEngine;
using System.Collections;

public class BlobController : MonoBehaviour {

	//private float moveForce = 365.0f;
	public Vector3 offset;			// The offset at which the Health Bar follows the player.

	private float attackAnimDist = 2f;
	private float velocityMultipler = 0.01f;

	private Animator anim;
	private Transform player;

    // Use this for initialization
	void Start () {
		//Debug.Log("Getting player object");
		player = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator>();
		//InvokeRepeating("bloop", 0f, 1.0f);        
        
    }

	void Update() {
		float dx = transform.position.x - player.position.x;
		if (Mathf.Abs(dx) < attackAnimDist ) {
			anim.SetBool ("attacking",true);
		}
		else {
			anim.SetBool ("attacking", false);
		}

		Vector2 direction;
		if(dx < 0) {
			anim.SetBool ("left", true);
			direction = -Vector2.right;
		}
		else {
			anim.SetBool ("left",false);
			direction = Vector2.right;
		}

		// Every second, we move for about a 1/2 second 
		var mseconds = 1000.0f*(Time.time - (int)Time.time);
		// Lets make speed increase linearly as we get to the half second mark.
		//var vel = rigidbody2D.velocity;
		var pos = transform.position;
		if(mseconds < 500) {
			pos.x += Time.deltaTime*direction.x*velocityMultipler*(mseconds - 500f);
			//vel.x = direction.x*velocityMultipler*(mseconds - 500f);
		}
		//rigidbody2D.velocity = vel;
		transform.position = pos;
		//Debug.Log("Velocity: " + vel);

	}
	

	// Update is called once per frame
//	void FixedUpdate () {



	//	transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z) ;
	
//	}




}
