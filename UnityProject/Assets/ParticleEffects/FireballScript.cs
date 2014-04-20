using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour {

	private float generateParticlesFor = 1.0f;
	private float killAfter = 3.0f;

	public int fireballLvl = 1;
	private int damangePerLvl = 35;

	// Use this for initialization
	void Start () {
		//Debug.Log ("Fireball firing!");

		int mask = 0;
		mask |= (1 << LayerMask.NameToLayer("Player"));
		mask |= (1 << LayerMask.NameToLayer("Enemies"));

		// Damage everything under fireball
		var stuffHit = Physics2D.OverlapCircleAll(transform.position,
		                                          radius: 1.25f,
		                                          layerMask: mask);

		foreach(Collider2D hit in stuffHit) {
			//Debug.Log ("Hit with fireball: " + hit);
			var hitGO = hit.gameObject;
			foreach (Component comp in hitGO.GetComponents<Component>()){
				//Debug.Log ("Got Comp: " + comp);
				if (comp is IDamagable) {
					//Debug.Log("Damagable");
					((IDamagable)comp).takeDamage(calcDamage());
				}
			}
		}

		Invoke ("turnOffEffect", generateParticlesFor);
		Invoke ("kill", killAfter);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private int calcDamage() {
		//TODO: Randomize?
		return damangePerLvl*fireballLvl;
	}

	void turnOffEffect() {
		foreach (Transform child in transform) {
			var epe = child.GetComponent<ParticleEmitter>();
			epe.emit = false;
		}

	}

	void kill() {
		Destroy(gameObject);
	}
}
