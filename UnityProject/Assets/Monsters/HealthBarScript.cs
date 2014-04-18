using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	private MonsterAttributes attrs;
	public Transform barForeground;
	private float xoffset = 0.5f;

	// Use this for initialization
	void Start () {
		attrs = transform.parent.GetComponent<MonsterAttributes>();
		var bounds = transform.parent.renderer.bounds;
		transform.position = new Vector3(bounds.center.x + xoffset, bounds.max.y, bounds.center.z);
		transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if(attrs.healthFraction != 1.0f) {
			transform.localScale = Vector3.one;
		}
		barForeground.localScale = new Vector3(attrs.healthFraction, 1.0f, 1.0f);
		((SpriteRenderer)barForeground.renderer).color = Color.Lerp(Color.red, Color.green, attrs.healthFraction);
	}
}
