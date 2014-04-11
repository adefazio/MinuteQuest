using UnityEngine;
using System.Collections;

public class CreateFloor : MonoBehaviour {
	public float startx = 80;
	public float starty = 80;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (startx, starty, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
