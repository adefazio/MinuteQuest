using UnityEngine;
using System.Collections;

public class LevelUpMessageScript : MonoBehaviour {
	GUIStyle textFont;
	
	private float messageTime = 2.0f;
	
	private int halfW = 1920/2;
	private int halfH = 1080/2;
	//private int swidth = 1920;
	private int sheight = 1080;
	private float startTime;
	
	void setupFonts() {
		textFont = new GUIStyle();
		var sizeMultipler = 1 + Time.time - startTime;
		textFont.fontSize = (int)(90.0f*sizeMultipler);
		textFont.alignment = TextAnchor.MiddleCenter;
		textFont.normal.textColor = Color.blue;
	}
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}

	void Update() {
		if(startTime + messageTime < Time.time) {
			Destroy(gameObject);
		}
	}

	void OnGUI() {
		setupFonts ();
		
		// All measurements are in reference to a 1080p screen. 
		//This resizes the units to be correct for other screen sizes.
		Vector2 resizeRatio = new Vector2(((float)Screen.width)/1920.0f, 
		                                  ((float)Screen.height/1080.0f));
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, 
		                           new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));

		var levelTxt = "Level Up!";
		GUI.Label(new Rect(halfW-300, halfH-100, 600, 127), levelTxt, textFont);
	}
}
