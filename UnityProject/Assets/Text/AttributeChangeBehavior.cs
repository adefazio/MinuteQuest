using UnityEngine;
using System.Collections;

public class AttributeChangeBehavior : MonoBehaviour {

	public float opacity = 1.0f;
	public float height = 1.0f;
	public float maxHeight = 0.8f;

	public string contents = "42";
	private Vector3 startingLocation;

	// Use this for initialization
	void Start () {
		startingLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		var text = GetComponent<TextMesh>();
		text.text = contents;
		var textColor = text.color;
		textColor.a = opacity;
		text.color = textColor;

		transform.position = startingLocation + maxHeight*height*Vector3.up;

		if(opacity < 0.0) {
			Destroy(gameObject, 1.0f);
		}
	}
}
