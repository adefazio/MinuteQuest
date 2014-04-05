using UnityEngine;
using System.Collections;

public class GUILayout : MonoBehaviour {

	private GUIStyle style;
	private Texture2D texture;
	private int selectedWeapon = 0;
	private int selectedSpell = 0;

	private string[] weaponNames = {"Sword", "Bow"};
	private string[] spellNames = {"Fireball", "Disguise"};

	void OnGUI ()
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
		GUI.BeginGroup(new Rect(10,10,90,70));
		GUI.Box(new Rect(0,0,90, 58), "");

		if(GUI.Button(new Rect(5,5,80,20), "Menu")) {
			//TODO
		}
		if(GUI.Button(new Rect(5,33,80,20), "Inventory")) {
			//TODO
		}

		GUI.EndGroup();

		/////////////////////
		/// Bars
		var defaultColor = GUI.color;
		GUI.BeginGroup(new Rect(Screen.width/2 - 50, Screen.height - 120, 100, 120));
		GUI.color = Color.red;
		GUI.Box(new Rect(0,0,40,120), "", style);
		GUI.color = Color.blue;
		GUI.Box(new Rect(60,0,40,120), "", style);
		GUI.color = defaultColor;

		GUI.EndGroup();

		//////////////////
		/// Select weapon
		var areaWidth = Screen.width / 3;
		GUI.BeginGroup(new Rect(20, Screen.height - 100, areaWidth, 80));
		GUI.Box(new Rect(0, 0, areaWidth, 80), "");

		selectedWeapon = GUI.SelectionGrid(new Rect (0, 0, areaWidth, 80), 
		                                   selectedWeapon, weaponNames, 2);

		GUI.EndGroup();

		/////////////////
		/// Select spell
		GUI.BeginGroup(new Rect(Screen.width - 20 - areaWidth, Screen.height - 100, areaWidth, 80));
		GUI.Box(new Rect(0, 0, areaWidth, 80), "");
		selectedSpell = GUI.SelectionGrid(new Rect (0, 0, areaWidth, 80), 
		                                  selectedSpell, spellNames, 2);
		GUI.EndGroup();
	}
}
