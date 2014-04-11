using UnityEngine;
using System.Collections;

public class CreateLevel : MonoBehaviour {

	public GameObject flatFloor;
	public GameObject flatRoof;
	public GameObject flatWall;
	public GameObject leftWall;
	public GameObject rightWall;
	public Transform t;
	private Transform player;
	private float tileWidth;
	private float tileHeight;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		SpriteRenderer s = flatWall.GetComponent<SpriteRenderer> ();
		tileWidth = s.bounds.size.x;
		tileHeight = s.bounds.size.y;
		t.position = new Vector3 (0f, 0f, 0f);
		Instantiate (leftWall, t.position + Vector3.left * tileWidth * 0.5f, t.rotation);
		for (int i = 0; i < 5; i++) {
			Instantiate (flatFloor, t.position + Vector3.down * tileHeight * 0.5f, t.rotation);
			Instantiate (flatWall, t.position, t.rotation);
			Instantiate (flatRoof, t.position + Vector3.up * tileHeight * 0.5f, t.rotation);
			t.position = t.position + Vector3.right * tileWidth;
		}
		Instantiate (rightWall, t.position + Vector3.right * tileWidth * 50.5f, t.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		if (player.position.x > t.position.x - tileWidth * 1.5f) {
			Instantiate (flatFloor, t.position + Vector3.down * tileHeight * 0.5f, t.rotation);
			Instantiate (flatWall, t.position, t.rotation);
			Instantiate (flatRoof, t.position + Vector3.up * tileHeight * 0.5f, t.rotation);
			t.position = t.position + Vector3.right * tileWidth;
		}
	}
}
