using UnityEngine;
using System.Collections;

public class CreateFlatSection : MonoBehaviour {

	public GameObject flatFloor;
	public GameObject flatRoof;
	public GameObject flatWall;
	public Transform t;
	// Use this for initialization
	void Start () {
		Instantiate (flatFloor, t.position + Vector3.down * 2.35f, t.rotation);
		Instantiate (flatWall, t.position, t.rotation);
		Instantiate (flatRoof, t.position + Vector3.up * 2.35f, t.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
