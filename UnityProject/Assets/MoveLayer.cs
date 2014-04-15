using UnityEngine;
using System.Collections;

public class MoveLayer : MonoBehaviour {

	//speed at which the layer moves
	//speed 1 means move with camera
	//speed 0 means stay fixed in scene
	public float speed = 0.5f;
	private Camera mainCamera;
	private Vector3 cameraPosition;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindObjectOfType<Camera>();
		cameraPosition = mainCamera.transform.position;
	}

	// Update is called once per frame
	void Update () {
		transform.position += (mainCamera.transform.position - cameraPosition) * speed;
		cameraPosition = mainCamera.transform.position;
	}
}
