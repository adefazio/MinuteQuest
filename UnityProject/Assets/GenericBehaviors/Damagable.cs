
using UnityEngine;
using System.Collections;

abstract public class Damagable : MonoBehaviour, IDamagable {

	abstract public int health { get; set;}

	public void takeDamage(float damage) {
		var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath(
			"Assets/Text/HealthLost.prefab", typeof(GameObject));
		
		var dmgint = (int)damage;
		var healthLoss = Instantiate(prefab,
		                             transform.position + Vector3.up,
		                             Quaternion.identity) as GameObject;
		var hlbehavior = healthLoss.GetComponent<HealthLostBehavior>();
		hlbehavior.contents = dmgint.ToString();
		
		health -= dmgint;
	}
}