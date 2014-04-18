using UnityEngine;
using System.Collections;

public class ExplodeOnDeath : MonoBehaviour {

	private Object fireballPrefab;
	private MonsterAttributes attrs;

	// Use this for initialization
	void Start () {
	
		fireballPrefab = Resources.Load("ParticleEffects/Fireball", typeof(GameObject));

		attrs = GetComponent<MonsterAttributes>();
		attrs.deathOccured += new MonsterAttributes.DeathEventHandler(OnDeath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDeath() {
		var lvl = attrs.level;
		var fb = Instantiate(fireballPrefab, transform.position, Quaternion.identity) as GameObject;
		fb.GetComponent<FireballScript>().fireballLvl = lvl;
	}
}
