using UnityEngine;
using System.Collections;

public class CreateLevel : MonoBehaviour {

	public GameObject flatFloor;
	public GameObject flatRoof;
	public GameObject flatWall;
	public GameObject leftWall;
	public GameObject rightWall;
	public Transform t;

	void Start () {
		t.position = new Vector3 (0f, 0f, 0f);
		Instantiate (leftWall, t.position + Vector3.left * 3.75f, t.rotation);
		for (int i = 0; i < 10; i++) {
			Instantiate (flatFloor, t.position + Vector3.down * 2.35f, t.rotation);
			Instantiate (flatWall, t.position, t.rotation);
			Instantiate (flatRoof, t.position + Vector3.up * 2.35f, t.rotation);
			t.position = t.position + Vector3.right * 7.5f;
		}
		Instantiate (rightWall, t.position + Vector3.left * 3.75f, t.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
