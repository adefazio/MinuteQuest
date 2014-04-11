using UnityEngine;
using System.Collections;

public class BlobController : MonoBehaviour {

	public float moveForce = 365.0f;
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	private Animator anim;
	public float attackAnimDist = 2f;
	
	private Transform player;		// Reference to the player.

	
	void bloop () {
		//Debug.Log ("Updating bloop");
		float dx = transform.position.x - player.position.x;
		if (Mathf.Abs(dx) < attackAnimDist ) {
			anim.SetBool ("attacking",true);
		}
		else {
			anim.SetBool ("attacking", false);
		}
		if(dx < 0) {
			anim.SetBool ("left", true);
			rigidbody2D.AddForce(Vector2.right * moveForce);
		}
		else {
			anim.SetBool ("left",false);
			rigidbody2D.AddForce(-Vector2.right * moveForce);
        }


    }
    // Use this for initialization
	void Start () {
		//Debug.Log("Getting player object");
		player = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator>();
		InvokeRepeating("bloop", 0f, 1.0f);        
        
    }

	

	// Update is called once per frame
//	void FixedUpdate () {



	//	transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z) ;
	
//	}




}
