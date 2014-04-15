
using UnityEngine;
using System.Collections;

abstract public class Damagable : MonoBehaviour, IDamagable {

	abstract public int health { get; set;}

	public void takeDamage(int damage) {
		var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath(
			"Assets/Text/HealthLost.prefab", typeof(GameObject));

		var hlpoint = renderer.bounds.center;
		hlpoint.y = renderer.bounds.max.y;

		var modvalue = this.GetComponent<ItemSpawner> ().newItem.GetComponent<Item> ().iValue;
		var dmgint = (int) (damage * modvalue);

		var healthLoss = Instantiate(prefab,
		                             hlpoint,
		                             Quaternion.identity) as GameObject;
		var hlbehavior = healthLoss.GetComponent<HealthLostBehavior>();
		Debug.Log("Spawning healthlost thingy: " + healthLoss);
		hlbehavior.contents = dmgint.ToString();
		
		health -= dmgint;
	}
}
