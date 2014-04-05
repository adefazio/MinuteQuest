using UnityEngine;
using System.Collections;

public class LockCamera : MonoBehaviour {

	public Transform player;
	
	// Update is called once per frame
	void Update () {
		var pos = camera.transform.position;
		camera.transform.position = new Vector3(player.position.x, pos.y, pos.z);
	}
}
