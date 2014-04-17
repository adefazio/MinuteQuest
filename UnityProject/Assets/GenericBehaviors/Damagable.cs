
using UnityEngine;
using System.Collections;

abstract public class Damagable : MonoBehaviour, IDamagable {

	abstract public int health { get; set;}

	public void takeDamage(int damage) {
		var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath(
			"Assets/Text/HealthLost.prefab", typeof(GameObject));

		var hlpoint = renderer.bounds.center;
		hlpoint.y = renderer.bounds.max.y + 0.3f;
		
		var dmgint = damage;
		/*
		var itmSpawner = this.GetComponent<ItemSpawner> ();
		if(itmSpawner != null) {
			var modvalue = this.GetComponent<ItemSpawner> ().newItem.GetComponent<Item> ().iValue;
			dmgint = (int) (damage * modvalue);
		} else {
			dmgint = damage;
		}
		*/

		var healthLoss = Instantiate(prefab,
		                             hlpoint,
		                             Quaternion.identity) as GameObject;
		var hlbehavior = healthLoss.GetComponent<AttributeChangeBehavior>();
		//Debug.Log("Spawning healthlost thingy: " + healthLoss);
		hlbehavior.contents = dmgint.ToString();
		
		health -= dmgint;
	}
}
