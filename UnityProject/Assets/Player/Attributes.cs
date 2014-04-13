using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour {

	public float health;
	public float mana;

	public float maxHealth;
	public float maxMana;

	public int level = 1;
	public int xp = 0;

	public int constitution = 10;
	public int strength = 10;
	public int energy = 10;

	public int attrPerLevel = 3;
	public float healthPerConst = 10.0f;
	public float manaPerEnergy = 10.0f;

	// Use this for initialization
	void Start () {
		maxHealth = constitution*healthPerConst;
		maxMana = energy*manaPerEnergy;

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

	public float xpLevelFraction {
		get { 
			var lvlXpRange = (float)(xpForLevel(level+1) - xpForLevel(level));
			return (xp - xpForLevel(level))/lvlXpRange; }
	}

	public int xpForLevel(int lvl) {
		return lvl*100;
	}

	public int xpToNextLevel() {
		return xpForLevel(level+1) - xp;
	}

	
	private void levelUp(){
		level += 1;
		
		strength += attrPerLevel;
		constitution += attrPerLevel;
		energy += attrPerLevel;
		
		maxHealth = constitution*healthPerConst;
		maxMana = energy*manaPerEnergy;
		
		// Most games reset your health to full
		// when you level up.
		health = maxHealth;
		mana = maxMana;

		var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath(
			"Assets/Text/LevelUpMessage.prefab", typeof(GameObject));
		
		Instantiate(prefab, transform.position, Quaternion.identity);

	}

	public void giveXP(int xpAdded) {
		xp += xpAdded;
		while(xpToNextLevel() <= 0) {
			levelUp();

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
