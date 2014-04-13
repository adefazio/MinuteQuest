using UnityEngine;
using System.Collections;

public class GUILayout : MonoBehaviour {

	private GameObject player;

	private GUIStyle whiteStyle;
	private int selectedWeapon = 0;
	private int selectedSpell = 0;

	GUIStyle bigTextFont;
	GUIStyle smallTextFont;
	GUIStyle smallestTextFont;
	private Color xpBarColor;

	private float splashScreenTime = 0.5f;
	private float levelTime = 5*60.0f;
	private float startTime = 0.0f;
	public Texture2D introSplashTexture;
	public Texture2D failureTexture;
	public Texture2D deathSplashTexture;
	private Texture2D alpha0;

	private string[] weaponNames = {"Saber", "Ray Gun"};
	private string[] spellNames = {"Fireball", "Disguise"};

	private int halfW = 1920/2;
	//private int halfH = 1080/2;
	private int swidth = 1920;
	private int sheight = 1080;
	private GUIStyle blackStyle;

	private int buttonHeight = 200;

	private Texture2D colorTexture(Color c) {
		var texture = new Texture2D(1, 1);
		texture.SetPixel(1, 1, c);
		texture.Apply();
		return texture;
	}

	public void Start() {

		startTime = Time.time;
		Debug.Log ("start time: " + startTime);

		whiteStyle = new GUIStyle();
		whiteStyle.normal.background = colorTexture(Color.white);

		player = GameObject.FindGameObjectWithTag("Player");

		blackStyle = new GUIStyle();
		blackStyle.normal.background = colorTexture(Color.black);

		alpha0 = colorTexture(Color.clear);

		xpBarColor = Color.green;
		xpBarColor.a = 0.3f;
	}

	void setupFonts() {
		bigTextFont = new GUIStyle(GUI.skin.button);
		bigTextFont.fontSize = 90;
		bigTextFont.alignment = TextAnchor.MiddleCenter;
		bigTextFont.normal.textColor = Color.white;

		smallTextFont = new GUIStyle(bigTextFont);
		smallTextFont.fontSize = 30;
		smallTextFont.normal.background = alpha0;

		smallestTextFont = new GUIStyle(smallTextFont);
		smallestTextFont.fontSize = 24;
	}

	void showGUI ()
	{
		var defaultColor = GUI.color;
		var pAttrs = player.GetComponent<Attributes>();
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

		///////////////
		/// Timer
		var ts = System.TimeSpan.FromSeconds(levelTime - Time.time + startTime);
		var timeStr = string.Format("{0}:{1}", ts.Minutes.ToString(), ts.Seconds.ToString());
		GUI.Box(new Rect(halfW-156, 0, 312, 127), timeStr, bigTextFont);


		// Bottom bar group
		GUI.BeginGroup(new Rect(0, sheight - 300, swidth, 300));

		///////////////
		/// XP Bar
		GUI.BeginGroup(new Rect(20, 10, swidth-40, 50));
		GUI.Box(new Rect(0, 0, swidth-20, 50), "", blackStyle);
		GUI.color = xpBarColor;
		//Debug.Log ("XP Fraction: " + pAttrs.xpLevelFraction);
		//Debug.Log ("XP: " + pAttrs.xp + " to next level: " + pAttrs.xpToNextLevel());
		var xpWidth = (swidth-60)*pAttrs.xpLevelFraction;
		GUI.Box(new Rect(5, 5, 10, 40), "", whiteStyle);
		GUI.Box(new Rect(15, 5, xpWidth, 40), "", whiteStyle);

		GUI.color = defaultColor;
		//Debug.Log ("level " + pAttrs.level); 
		var xpText = "Lvl " + pAttrs.level + "     XP " + pAttrs.xp;
		GUI.Label(new Rect(5, 5, swidth-40, 40), xpText, smallTextFont);
		GUI.EndGroup();

		// Rest of bottom bar
		GUI.BeginGroup(new Rect(0, 50, swidth, 250));

		/////////////////////
		/// Health and mana bars
		GUI.BeginGroup(new Rect(halfW - 67, 18, 134, buttonHeight+4));
		GUI.Box(new Rect(0, 0, 64, buttonHeight+4), "", blackStyle);
		GUI.Box(new Rect(134, 0, -64, buttonHeight+4), "", blackStyle);
		GUI.BeginGroup(new Rect(2, 2, 130, buttonHeight));
        GUI.color = Color.red;
		//Debug.Log("health " + pAttrs.healthFraction);

		GUI.Box(new Rect(0,buttonHeight,60,-buttonHeight*pAttrs.healthFraction), "", whiteStyle);
		GUI.color = Color.blue;
		GUI.Box(new Rect(70,buttonHeight,60,-buttonHeight*pAttrs.manaFraction), "", whiteStyle);
		GUI.color = defaultColor;

		// Health text
		GUI.Label(new Rect(0, 70, 60, 30), 
		          ((int)pAttrs.health).ToString(), smallestTextFont);
		GUI.Box(new Rect(5,100,50,2), "", whiteStyle);
		GUI.Label(new Rect(0, 110, 60, 30), 
		          ((int)pAttrs.maxHealth).ToString(), smallestTextFont);

		// Mana Text
		GUI.Label(new Rect(65, 70, 60, 30), 
		          ((int)pAttrs.mana).ToString(), smallestTextFont);
		GUI.Box(new Rect(75,100,50,2), "", whiteStyle);
		GUI.Label(new Rect(65, 110, 60, 30), 
		          ((int)pAttrs.maxMana).ToString(), smallestTextFont);

		GUI.EndGroup();
		GUI.EndGroup();

		//////////////////
		/// Select weapon
		var areaWidth = halfW-100;
		GUI.BeginGroup(new Rect(20, 0, areaWidth, 300));
		GUI.Box(new Rect(20, 20, areaWidth-20, buttonHeight), "");
		
		selectedWeapon = GUI.SelectionGrid(new Rect (20, 20, areaWidth-20, buttonHeight), 
		                                   selectedWeapon, weaponNames, 2, bigTextFont);
		GUI.EndGroup();

		/////////////////
		/// Select spell

		GUI.BeginGroup(new Rect(halfW + 60, 0, areaWidth, 300));
		GUI.Box(new Rect(20, 20, areaWidth-20, buttonHeight), "");
		
		selectedSpell = GUI.SelectionGrid(new Rect (20, 20, areaWidth-20, buttonHeight), 
		                                  selectedSpell, spellNames, 2, bigTextFont);
		GUI.EndGroup();

		GUI.EndGroup();
		GUI.EndGroup();

	}

	void resetGame() {
		Application.LoadLevel(0);
	}
	
	void OnGUI() {
		setupFonts ();
		
		// All measurements are in reference to a 1080p screen. 
		//This resizes the units to be correct for other screen sizes.
		Vector2 resizeRatio = new Vector2(((float)Screen.width)/1920.0f, 
		                                  ((float)Screen.height/1080.0f));
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, 
		                           new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));

		var pAttrs = player.GetComponent<Attributes>();
		if(pAttrs.health <= 0){
			GUI.DrawTexture(new Rect(0,0,1920,1080), deathSplashTexture, ScaleMode.ScaleToFit, true, 0.0f);
			Invoke("resetGame", 5.0f);
			return;
		}

		if (Time.time > levelTime + startTime) {
			Time.timeScale = 0.0f;
			GUI.DrawTexture(new Rect(0,0,1920,1080), failureTexture, ScaleMode.ScaleToFit, true, 0.0f);
			// Additional code for restarting the game, going to the menu screen, saving loot etc.
			// goes here.
		} else {
			if (Time.realtimeSinceStartup > splashScreenTime) {
				Time.timeScale = 1.0f;
				showGUI ();
			} else {
				Time.timeScale = 0.0f;
				GUI.DrawTexture(new Rect(0,0,1920,1080), introSplashTexture, ScaleMode.ScaleToFit, true, 0.0f);
			}
		}


	}
}
