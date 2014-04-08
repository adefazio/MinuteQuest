using UnityEngine;
using System.Collections;

public class LockCamera : MonoBehaviour {

	public Transform player;
	public enum CameraTypes { Centered, ForwardLooking };
	public CameraTypes cameraType = CameraTypes.Centered;

	// Update is called once per frame
	void Update () {
		switch(cameraType){
			case CameraTypes.Centered:
				var pos = camera.transform.position;
				camera.transform.position = new Vector3(player.position.x, pos.y, pos.z);
				break;
		}
	}
}
