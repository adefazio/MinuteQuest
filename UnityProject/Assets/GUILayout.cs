using UnityEngine;
using System.Collections;

public class GUILayout : MonoBehaviour {

	private GUIStyle style;
	private Texture2D texture;
	private int selectedWeapon = 0;
	private int selectedSpell = 0;

	GUIStyle textFont;

	private float splashScreenTime = 3.0f;
	private float levelTime = 5*60.0f;
	public Texture2D introSplashTexture;

	private string[] weaponNames = {"Saber", "Ray Gun"};
	private string[] spellNames = {"Fireball", "Disguise"};

	private int halfW = 1920/2;
	private int halfH = 1080/2;
	private int swidth = 1920;
	private int sheight = 1080;

	public void Start() {

	}

	void setupFonts() {
		textFont = new GUIStyle(GUI.skin.button);
		textFont.fontSize = 90;
		textFont.alignment = TextAnchor.MiddleCenter;
		textFont.normal.textColor = Color.white;
	}

	void showGUI ()
	{

		if(texture == null){
			texture = new Texture2D(1, 1);
		}
		if(style == null){
			style = new GUIStyle();
		}

		texture.SetPixel(1, 1, Color.white);
		texture.Apply();
		
		style.normal.background = texture;


		// Menu box
		/* Hide for now
		GUI.BeginGroup(new Rect(10,10,90,70));
		GUI.Box(new Rect(0,0,90, 58), "");

		if(GUI.Button(new Rect(5,5,80,20), "Menu")) {
			//TODO
		}
		if(GUI.Button(new Rect(5,33,80,20), "Inventory")) {
			//TODO
		}
		GUI.EndGroup();
		*/



		/////////////////////
		/// Bars
		var defaultColor = GUI.color;
		GUI.BeginGroup(new Rect(halfW - 50, sheight - 300, 100, 300));
		GUI.color = Color.red;
		GUI.Box(new Rect(0,20,40,250), "", style);
		GUI.color = Color.blue;
		GUI.Box(new Rect(60,20,40,250), "", style);
		GUI.color = defaultColor;

		GUI.EndGroup();

		//////////////////
		/// Select weapon
		var areaWidth = halfW-100;
		GUI.BeginGroup(new Rect(20, sheight-300, areaWidth, 300));
		GUI.Box(new Rect(20, 20, areaWidth-20, 250), "");
		
		selectedWeapon = GUI.SelectionGrid(new Rect (20, 20, areaWidth-20, 250), 
		                                   selectedWeapon, weaponNames, 2, textFont);
		GUI.EndGroup();

		/////////////////
		/// Select spell

		GUI.BeginGroup(new Rect(halfW + 60, sheight-300, areaWidth, 300));
		GUI.Box(new Rect(20, 20, areaWidth-20, 250), "");
		
		selectedWeapon = GUI.SelectionGrid(new Rect (20, 20, areaWidth-20, 250), 
		                                   selectedSpell, spellNames, 2, textFont);
		GUI.EndGroup();

		///////////////
		/// Timer
		var ts = System.TimeSpan.FromSeconds(levelTime  - Time.time);
		var timeStr = string.Format("{0}:{1}", ts.Minutes.ToString(), ts.Seconds.ToString());
		GUI.Box(new Rect(halfW-156, 0, 312, 127), timeStr, textFont);
	}

	
	void OnGUI() {
		setupFonts ();
		
		// All measurements are in reference to a 1080p screen. 
		//This resizes the units to be correct for other screen sizes.
		//Debug.Log ("Screen width " + Screen.width + " height " + Screen.height);
		Vector2 resizeRatio = new Vector2(((float)Screen.width)/1920.0f, 
		                                  ((float)Screen.height/1080.0f));
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, 
		                           new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));

		if (Time.realtimeSinceStartup > splashScreenTime) {
			Time.timeScale = 1.0f;
			showGUI ();
		} else {
			Time.timeScale = 0.0f;
			GUI.DrawTexture(new Rect(0,0,1920,1080), introSplashTexture, ScaleMode.ScaleToFit, true, 0.0f);
		}
	}
}
