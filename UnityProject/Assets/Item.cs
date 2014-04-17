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

	public int iStat;
	public Stat iEnum;
	public float iValue;
	public float iLevel = 1.0f;
	public string iName;
	public float iTimeout = 30.0f;

	[HideInInspector]
	public bool dropped = false;

	private System.Random r = new System.Random ();
	//private Transform tPlayer;
	private float dropTime = 0.0f;

	// Use this for initialization
	void Start () {
		Array stats = Enum.GetValues(typeof(Stat));
		iStat = r.Next (stats.Length);
		iEnum = (Stat)stats.GetValue (iStat);
		iValue = (float)r.NextDouble () * iLevel;
		iName = iEnum.ToString () + " GEM";
		//tPlayer = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (dropped) {
			if (dropTime == 0.0f) {
				dropTime = Time.time;
			}
			else if (Time.time > dropTime + iTimeout) {
				Destroy(gameObject);
			}
		}
	}

}
