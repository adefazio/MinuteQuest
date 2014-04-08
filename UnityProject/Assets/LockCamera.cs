using UnityEngine;
using System.Collections;

public class LockCamera : MonoBehaviour {

	public Transform player;
	public enum CameraTypes { Centered, ForwardLooking };
	private CameraTypes cameraType = CameraTypes.ForwardLooking;

	private float scrollSpeed = 15.0f;
	private float cameraForwardness = 2.0f;

	// Update is called once per frame
	public void LateUpdate () {
		switch(cameraType){
			case CameraTypes.Centered:
				var pos = camera.transform.position;
				camera.transform.position = new Vector3(player.position.x, pos.y, pos.z);
				break;
			case CameraTypes.ForwardLooking:
				float cameraDest = player.position.x;
				Actions pActions = player.GetComponent<Actions>();
				cameraDest += pActions.isFacingRight ? cameraForwardness : -cameraForwardness; 

				var cpos = camera.transform.position;
				if(cpos.x != cameraDest) {
					if(Mathf.Abs(cameraDest - cpos.x) >  Time.deltaTime*scrollSpeed) {
						cpos.x += Time.deltaTime*scrollSpeed*Mathf.Sign(cameraDest - cpos.x);
					} else {
						cpos.x = cameraDest;
					}
				}
				camera.transform.position = cpos;
			break;
		}
	}
}
