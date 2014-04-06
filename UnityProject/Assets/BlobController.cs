using UnityEngine;
using System.Collections;

public class BlobController : MonoBehaviour {

	public float blobSpeed = .02f;
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	
	private Transform player;		// Reference to the player.

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {



		transform.position = player.position + offset;
	
	}




}
