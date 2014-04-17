using UnityEngine;
using System.Collections;

public class MonsterAttributes : Damagable {

	public int _health;
	public int worthXP;

	public int level = 1;
	public float physicalScaling = 1.0f;
	public float speedScaling = 1.0f;
	public float damageScaling = 1.0f;
	public float healthScaling = 1.0f;

	private float critChance = 0.03f;
	private int baseDmg = 10;
	private int dmgPerLevel = 3;
	private int healthPerLevel = 10;
	private int xpPerLevel = 35;

	private float statOrbDropChance = 0.2f;

	public override int health {
		get { return _health; }
		set { _health = value; }
	}

	private float nextFloat(float x, float y) {
		return x + Random.value * (y-x);
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Monster start, lvl: " + level);

		// Make things more random
		speedScaling *= nextFloat(0.8f, 1.2f);
		damageScaling *= nextFloat(0.8f, 1.2f);
		physicalScaling *= nextFloat(0.8f, 1.2f);

		health = (level + 3) * healthPerLevel;
		health = (int)(healthScaling*physicalScaling*health);
		transform.localScale *= physicalScaling;

		worthXP = (int)(level * xpPerLevel * nextFloat(0.5f, 2.0f));


	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {

			var player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<Attributes>().giveXP(worthXP);

			// Drop health/mana orbs
			if(Random.value < statOrbDropChance) {
				OrbScript.spawnRandomOrb(transform.position, level);
			}

			//TODO: animate?
			Destroy(gameObject);

		}
	}

	public int getRandomAttackDamage() {
		var dmg = baseDmg + level*dmgPerLevel;

		if(Random.value < critChance) {
			dmg = dmg*2; // Critical HIT!!!!!
		}

		// Add randomness
		dmg = (int)(dmg * nextFloat(0.6f, 1.4f));

		return dmg;
	}


}
