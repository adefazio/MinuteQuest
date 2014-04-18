using UnityEngine;
using System.Collections;

public class Attributes : Damagable {

	public int _health = 1;
	public int _mana;
	public int manaPerSecondPercentage = 3;

	public int maxHealth;
	public int maxMana;
	public bool isDisguised = false;

	[HideInInspector]
	public int level = 1;
	[HideInInspector]
	public int xp = 0;

	[HideInInspector]
	public int constitution = 10;
	[HideInInspector]
	public int strength = 10;
	[HideInInspector]
	public int energy = 10;

	private int attrPerLevel = 3;
	private int healthPerConst = 10;
	private int manaPerEnergy = 10;

	public override int health {
		get { return _health; }
		set { 
			_health = value; 
			if(_health > maxHealth)
				_health = maxHealth;
		}
	}

	public int mana {
		get { return _mana; }
		set { 
			_mana = value; 
			if(_mana > maxMana)
				_mana = maxMana;
			if(_mana < 0)
				_mana = 0;
		}
	}

	// Use this for initialization
	void Start () {
		maxHealth = constitution*healthPerConst;
		maxMana = energy*manaPerEnergy;

		health = maxHealth;
		mana = maxMana;

		InvokeRepeating("addMana", 0f, 1.0f);   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float healthFraction { 
		get { return health/((float)maxHealth); }
	}
	public float manaFraction { 
		get { return mana/((float)maxMana); }
	}

	public float xpLevelFraction {
		get { 
			var lvlXpRange = (float)(xpForLevel(level+1) - xpForLevel(level));
			return (xp - xpForLevel(level))/lvlXpRange; }
	}

	public int xpForLevel(int lvl) {
		return (lvl-1)*100;
	}

	public int xpToNextLevel() {
		return xpForLevel(level+1) - xp;
	}

	public void addMana() {
		mana += (int)(maxMana*(manaPerSecondPercentage/100.0f));
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

		var prefab = Resources.Load("Text/LevelUpMessage", typeof(GameObject));
		
		Instantiate(prefab, transform.position, Quaternion.identity);

	}

	public void giveXP(int xpAdded) {
		xp += xpAdded;
		while(xpToNextLevel() <= 0) {
			levelUp();

		}
	}

}
