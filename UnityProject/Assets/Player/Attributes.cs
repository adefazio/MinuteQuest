using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour {

	public float health;
	public float mana;

	public float maxHealth;
	public float maxMana;

	public int level;
	public int xp;

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

	public int xpToNextLevel() {
		return level*100 - xp;
	}

	public void giveXP(int xpAdded) {
		xp += xpAdded;
		while(xpToNextLevel() < 0) {
			level += 1;
			// TODO: Level up overlay.
		}
	}
	
	public void takeDamage(float damage) {
		var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath(
			"Assets/Text/HealthLost.prefab", typeof(GameObject));
		
		var healthLoss = Instantiate(prefab,
		                             transform.position + Vector3.up,
		                             Quaternion.identity) as GameObject;
		var hlbehavior = healthLoss.GetComponent<HealthLostBehavior>();
		hlbehavior.contents = damage.ToString();

		health -= damage;
	}
}
