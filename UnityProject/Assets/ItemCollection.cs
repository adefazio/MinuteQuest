using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ItemCollection : MonoBehaviour {
	
	public float[] statTotals;
	public Array stats = Enum.GetValues (typeof(Item.Stat));

	// Use this for initialization
	void Start () {
		//Array stats = Enum.GetValues (typeof(Item.Stat));
		statTotals = new float[stats.Length];
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<Actions> ().itemCollected) {
			GameObject itemObject = gameObject.GetComponent<Actions> ().itemTarget;
			statTotals[itemObject.GetComponent<Item> ().iStat] += itemObject.GetComponent<Item> ().iValue;
			Destroy(itemObject);
			gameObject.GetComponent<Actions> ().itemCollected = false;
		}
	}
}
