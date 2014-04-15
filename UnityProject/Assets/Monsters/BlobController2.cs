using UnityEngine;
using System.Collections;

public class BlobController2 : MonoBehaviour {

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
		Debug.Log ("Blob2");
    }

	void Update() {
		float dx = transform.position.x - player.position.x;
		float direction = (dx > 0 ? 1.0f : -1.0f);

		if (Mathf.Abs(dx) < attackAnimDist ) {
			anim.SetBool ("attacking",true);
		}
		else {
			anim.SetBool ("attacking", false);

			transform.localScale = new Vector3(direction, 1.0f, 1.0f);

			// Every second, we move for about a 1/2 second 
			var mseconds = 1000.0f*(Time.time - (int)Time.time);
			// Lets make speed increase linearly as we get to the half second mark.
			var pos = transform.position;
			if(mseconds < 500) {
				pos.x += Time.deltaTime*direction*velocityMultipler*(mseconds - 500f);
            }
            transform.position = pos;
		}


	}
	

	// Update is called once per frame
//	void FixedUpdate () {



	//	transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z) ;
	
//	}




}
