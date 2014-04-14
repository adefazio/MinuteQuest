﻿using UnityEngine;
using System.Collections;

public class Actions : MonoBehaviour {

	private Camera mainCamera;
	private Object fireballPrefab;
	private Attributes attrs;
	public bool isJumping = false;
	public bool isFacingRight = true;
	private float jumpForce =  80000.0f;
	private float moveVelocity = 6.0f;
	private float bottomBarScreenFraction = 0.278f;
	private float howCloseToMonsterToMove = 1.0f;
	private int fireballMana = 20;
	private int fireballLvl = 1;
	private int attackDamage = 20;

	private Animator anim;					// Reference to the player's animator component.

	private bool movingToClick = false;
	private float playerMovementTarget = 0.0f;
	
	private bool attackingMonster = false;
	private Collider2D monsterTarget = null;

	[HideInInspector]
	public bool itemCollected = false;
	[HideInInspector]
	public GameObject itemTarget;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindObjectOfType<Camera>();
		fireballPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath(
			"Assets/ParticleEffects/Fireball.prefab", typeof(GameObject));
		attrs = GetComponent<Attributes>();
	}
	
	// Update is called once per frame
	void Update () {

		float h = Input.GetAxis("Horizontal");

		if(h != 0.0f) {
			// Stop movement to mouse target if direction keys are pressed.
			movingToClick = false;

			// Run!
			anim.SetBool("Running", true);
		}

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		if (h > 0.0f) {
			//Debug.Log ("Move right");

			if(isFacingRight) {
				transform.position += (Time.deltaTime*moveVelocity * Vector3.right);
			} else {
				//Flip to facing right
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}
			isFacingRight = true;
		} else if (h < 0.0f) {

			//Debug.Log ("Move left");
			if(!isFacingRight) {
				transform.position -= Time.deltaTime*moveVelocity * Vector3.right;
			} else {
				//Flip to facing left
				transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			}
			isFacingRight = false;
		}

		/////////////////////////////////////

		var jumpBtnDown = Input.GetButtonDown("Jump");

		if(jumpBtnDown && !isJumping) {
			Debug.Log("Jump triggered", gameObject);
			isJumping = true;
			movingToClick = false;
			rigidbody2D.AddForce(Vector2.up * jumpForce);
		}

		var attackBtnDown = Input.GetButtonDown("Fire1");
		if(attackBtnDown) {
			//!anim.GetCurrentAnimatorStateInfo(0).IsName("AttackAnimation")
			triggerAttack();
		}

		///////////////////////////////////////
		/// Mouse Controls
		var mousePos = Input.mousePosition;
		if(Input.GetMouseButtonDown(0)) {
			// Bar 
			if(mousePos.y > bottomBarScreenFraction*Screen.height) {
				Debug.Log(mousePos);
				mousePos.z = 0.0f; // select distance = 10 units from the camera
				var clickPos = mainCamera.ScreenToWorldPoint(mousePos);
				Debug.Log(clickPos);

				Collider2D[] col = Physics2D.OverlapPointAll(clickPos);

				var monsterClicked = false;
				foreach(Collider2D c in col) {
					if(c.gameObject.tag == "Enemy") {
						monsterClicked = true;
						attackingMonster = true;
						monsterTarget = c;
					}
					if(c.gameObject.tag == "Item") {
						itemCollected = true;
						itemTarget = c.gameObject;
					}

				}

				if(!monsterClicked) {
					movingToClick = true;
					playerMovementTarget = clickPos.x;

					if (playerMovementTarget - transform.position.x > 0) { 		
						transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
						isFacingRight = true;
					} else {
						transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
						isFacingRight = false;
					}
				}
			}
		}

		if(Input.GetMouseButtonDown(1)) {
			Debug.Log ("Mouse button 1 pressed");
			if(attrs.mana >= fireballMana) {
				attrs.mana -= fireballMana;
				mousePos.z = 0.0f;
				var clickPos = mainCamera.ScreenToWorldPoint(mousePos);
				var fb = Instantiate(fireballPrefab, clickPos, Quaternion.identity) as GameObject;
				fb.GetComponent<FireballScript>().fireballLvl = fireballLvl;
			}
		}

		if(attackingMonster) {
			Debug.Log ("Monster");
			Debug.Log (monsterTarget);
			Vector3 monsterpoint = monsterTarget.transform.position;
			var directionToPlayer = Mathf.Sign (transform.position.x - monsterpoint.x);
			Debug.Log("direction to Player: " + directionToPlayer);
			Debug.Log("playerMovementTarget: " + playerMovementTarget);
			playerMovementTarget = monsterpoint.x + directionToPlayer*howCloseToMonsterToMove;
			Debug.Log("player location: " + transform.position.x);
			Debug.Log ("monsterpoint: " + monsterpoint.x);
			//monsterpoint.x = playerMovementTarget;
			//Debug.DrawRay(monsterpoint, Vector3.up);
		}

		if(movingToClick || attackingMonster) {
			anim.SetBool("Running", true);
			var pos = transform.position;
			var posDelta = playerMovementTarget - pos.x;
			if(Mathf.Abs(posDelta) < Time.deltaTime*moveVelocity) {
				pos.x = playerMovementTarget;
				movingToClick = false;
				if(attackingMonster) {
					attackingMonster = false;
					triggerAttack();
				}

			} else {
				pos += Mathf.Sign(posDelta)*(Time.deltaTime*moveVelocity * Vector3.right);
			}
			transform.position = pos;
		} else {
			if(h == 0.0f) { // movement key not down
				anim.SetBool("Running", false);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("On Trigger Enter");
	}

	void OnCollisionEnter2D(Collision2D collision) {

		// Debug-draw all contact points and normals
		//foreach(ContactPoint2D contact in collision.contacts) {
			//Debug.DrawRay(contact.point, contact.normal, Color.white);
		//}

		//Debug.Log("Hit Something");
		
		if(isJumping && collision.gameObject.tag == "floor") {
			isJumping = false;
			Debug.Log("Hit floor");
		}
	}

	void triggerAttack(){
		anim.SetTrigger("Attack");
		Debug.Log ("Attack");
			
		int mask = (1 << LayerMask.NameToLayer("Enemies"));

		var posMod = isFacingRight ? Vector3.right : Vector3.left;
		posMod *= 1.0f;
		posMod += 0.5f*Vector3.up;

		// Damage everything under fireball
		var stuffHit = Physics2D.OverlapPointAll(
			  transform.position + posMod,
	          layerMask: mask);
		
		foreach(Collider2D hit in stuffHit) {
			Debug.Log ("Hit with attack: " + hit);
			var hitGO = hit.gameObject;
			hitGO.GetComponent<MonsterAttributes>().takeDamage(attackDamage);
		}

		//attrs.takeDamage(13);

		//attrs.giveXP(27);
	}

	
}
