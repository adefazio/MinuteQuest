using UnityEngine;
using System.Collections;

public class MonsterAttributes : Damagable {

	public int _health = 100;
	public int worthXP = 50;

	public override int health {
		get { return _health; }
		set { _health = value; }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {
			//TODO: animate?
			Destroy(gameObject);
		}
	}


}
