using UnityEngine;
using System.Collections;

public class OrbScript : MonoBehaviour {

	public enum OrbType { Health, Mana };
	public OrbType orbType = OrbType.Health; 
	public int attributeIncrease = 10;

	private Transform player;
	private float orbPickupRange = 1.0f;
	private Object gainTextPrefab;

	private static int attrPerLevel = 20;
	private static Object healthOrbPrefab = Resources.Load("Items/HealthOrb", typeof(GameObject));
	private static Object manaOrbPrefab = Resources.Load("Items/manaOrb", typeof(GameObject));

	public static void spawnRandomOrb(Vector3 pos, int level) {
		var orb = Instantiate (Random.value < 0.5 ? healthOrbPrefab : manaOrbPrefab,
		                       pos, Quaternion.identity) as GameObject;
		orb.GetComponent<OrbScript>().attributeIncrease = level*attrPerLevel;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;

		var asset = orbType == OrbType.Health ? 
			"Text/HealthGained.prefab" : "Text/ManaGained";

		gainTextPrefab =  Resources.Load(asset, typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position - player.position).magnitude < orbPickupRange) {
			var attrs = player.GetComponent<Attributes>();
			if(orbType == OrbType.Health) {
				attrs.health += attributeIncrease;
			} else {
				attrs.mana += attributeIncrease;
			}

			var textPoint = player.renderer.bounds.center;
			textPoint.y = player.renderer.bounds.max.y;

			var textObj = Instantiate(gainTextPrefab,
			                             textPoint,
			                             Quaternion.identity) as GameObject;
			var textBeh = textObj.GetComponent<AttributeChangeBehavior>();
			textBeh.contents = attributeIncrease.ToString();

			Destroy(gameObject);
		}
	}
}
