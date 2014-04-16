using UnityEngine;
using System.Collections;

public class LevelTile : MonoBehaviour {

	public GameObject tileObject;
	private Transform player;
	private float tileWidth;
	private float tileHeight;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		SpriteRenderer tileSprite = tileObject.GetComponent<SpriteRenderer> ();
		tileWidth = tileSprite.bounds.size.x;
		tileHeight = tileSprite.bounds.size.y;
		Instantiate (tileObject, transform.position, transform.rotation);
		Instantiate (tileObject, transform.position + Vector3.left * tileWidth, transform.rotation);
		transform.position += Vector3.right * tileWidth;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.position.x > transform.position.x - tileWidth * 1.5f) {
			Instantiate (tileObject, transform.position, transform.rotation);
			transform.position += Vector3.right * tileWidth;
		}

	}
}
