using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour {

	private float generateParticlesFor = 1.0f;
	private float killAfter = 3.0f;

	// Use this for initialization
	void Start () {

		// Damage everything under fireball
		//TODO

		Invoke ("turnOffEffect", generateParticlesFor);
		Invoke ("kill", killAfter);
	}
	
	// Update is called once per frame
	void Update () {
	
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
