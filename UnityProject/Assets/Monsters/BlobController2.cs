﻿using UnityEngine;
using System.Collections;

public class BlobController2 : MonoBehaviour {

	public Vector3 offset;			// The offset at which the Health Bar follows the player.

	private float attackAnimDist = 1.2f;
	private float velocityMultipler = 0.01f;

	private Animator anim;
	private Transform player;
	private float timeOffset;

    // Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator>();

		timeOffset = Random.value;
    }

	void Update() {
		float dx = transform.position.x - player.position.x;
		float direction = (dx > 0 ? 1.0f : -1.0f);

		if (Mathf.Abs(dx) < attackAnimDist ) {
			anim.SetBool ("attacking",true);
		}
		else {
			anim.SetBool ("attacking", false);

			transform.localScale = new Vector3(direction, 1.0f, 1.0f);

			// Every second, we move for about a 1/2 second 
			var t = Time.time + timeOffset;
			var mseconds = 1000.0f*(t - (int)t);
			// Lets make speed increase linearly as we get to the half second mark.
			var pos = transform.position;
			if(mseconds < 500) {
				pos.x += Time.deltaTime*direction*velocityMultipler*(mseconds - 500f);
            }
            transform.position = pos;
		}


	}

	public void hitPlayer() {
		float dx = transform.position.x - player.position.x;
		if (Mathf.Abs(dx) < attackAnimDist ) {
			//TODO hook damage to gem and monster level.
			var dmg = 10;
			player.GetComponent<Attributes>().takeDamage(dmg);
        }
    }
}
