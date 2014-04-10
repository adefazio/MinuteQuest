using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour {

	public float health;
	public float mana;

	public float maxHealth;
	public float maxMana;

	public int level;

	public int constitution = 10;
	public int strength = 10;
	public int energy = 10;

	// Use this for initialization
	void Start () {
		maxHealth = constitution*10.0f;
		maxMana = energy*10.0f;

		health = maxHealth;
		mana = maxMana;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float healthFraction { 
		get { return health/maxHealth; }
	}
	public float manaFraction { 
		get { return mana/maxMana; }
	}
}
