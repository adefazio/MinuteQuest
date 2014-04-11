using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*struct ItemStruct {
	public string name;
	public bool prefix;
	public Item.Stat stat;


}*/

public class Item : MonoBehaviour {

	public enum Stat
		{
			FIRE, ICE, WATER, RADIATION, ELECTRICITY, PSYCHIC, EARTH, STAGGERING, SMELLY
		}

	public Stat iStat;
	public float iValue;
	public float iLevel = 1.0f;
	public string iName;

	private System.Random r = new System.Random ();

	// Use this for initialization
	void Start () {
		Array stats = Enum.GetValues(typeof(Stat));
		iStat = (Stat)stats.GetValue (r.Next (stats.Length));
		iValue = (float)r.NextDouble () * iLevel;
		iName = iStat.ToString () + " GEM";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
