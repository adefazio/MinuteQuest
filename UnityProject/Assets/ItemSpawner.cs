using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	//GameObject gemPrefab = (GameObject) UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Items/Gem.prefab", typeof(GameObject));

	public GameObject gem;
	[HideInInspector]
	public GameObject newItem;
	private MonsterAttributes attrs;

	// Use this for initialization
	void Start () {
		newItem = Instantiate (gem,transform.position,Quaternion.identity) as GameObject;
		newItem.renderer.enabled = false;
		newItem.GetComponent<BoxCollider2D> ().enabled = false;

		attrs = GetComponent<MonsterAttributes>();
		attrs.deathOccured += new MonsterAttributes.DeathEventHandler(OnDeath);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnDeath () {
		newItem.transform.position = transform.position + Vector3.up;
		newItem.renderer.enabled = true;
		newItem.GetComponent<Item> ().dropped = true;
		newItem.GetComponent<BoxCollider2D> ().enabled = true;
	}

}
