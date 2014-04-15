using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	//GameObject gemPrefab = (GameObject) UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Items/Gem.prefab", typeof(GameObject));

	public GameObject gem;
	[HideInInspector]
	public GameObject newItem;

	// Use this for initialization
	void Start () {
		newItem = Instantiate (gem,transform.position,Quaternion.identity) as GameObject;
		newItem.renderer.enabled = false;
		newItem.GetComponent<BoxCollider2D> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Called on object destruction
	void OnDestroy () {
		newItem.transform.position = transform.position + Vector3.up;
		newItem.renderer.enabled = true;
		newItem.GetComponent<Item> ().dropped = true;
		newItem.GetComponent<BoxCollider2D> ().enabled = true;
	}

}
