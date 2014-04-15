using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterGenerator : MonoBehaviour {

	private Dictionary<int, bool> spawnedMonstersInSegment;

	private float segmentWidth = 50.0f;

	private System.Random rnd;
	private int gangsPerSegmentMin = 2;
	private int gangsPerSegmentMax = 5;
	private int monstersPerGangMin = 1;
	private int monstersPerGangMax = 5;
	private float gangRadius = 2.0f;

	private List<Object> monstersPrefabs = new List<Object>();

	// Use this for initialization
	void Start () {
		rnd = new System.Random();
		spawnedMonstersInSegment = new Dictionary<int, bool>();

		monstersPrefabs.Add(UnityEditor.AssetDatabase.LoadAssetAtPath(
			"Assets/Monsters/GreenBlob.prefab", typeof(GameObject)));
	}

	private int segment(Vector2 v) {
		return (int)System.Math.Round(v.x / segmentWidth, 1, System.MidpointRounding.AwayFromZero);
	}

	// Update is called once per frame
	void Update () {
		var pseg = segment (transform.position);
		for(int s = pseg-1; s<=pseg+1; s++) {
			if(!spawnedMonstersInSegment.ContainsKey(s)) {
				Debug.Log ("Entered new segment " + s);
				spawnMonstersInSegment(s);
			}
		}
	}

	private float nextFloat(float x, float y) {
		return x + ((float)rnd.NextDouble()) * (y-x);
	}

	void spawnMonstersInSegment(int s) {
		spawnedMonstersInSegment[s] = true;

		var ngangs = rnd.Next(gangsPerSegmentMin, gangsPerSegmentMax+1);

		for(int gang=0; gang < ngangs; gang++) {
			var nmonsters = rnd.Next(monstersPerGangMin, monstersPerGangMax+1);
			var gangPos = nextFloat(segmentWidth*s, segmentWidth*(s+1));

			for(int monster=0; monster < nmonsters; monster++) {
				var monsterPos = nextFloat(gangPos-gangRadius, gangPos+gangRadius);
				var monsterPrefab = monstersPrefabs[rnd.Next(monstersPrefabs.Count)];

				var mobj = Instantiate(monsterPrefab, new Vector3(monsterPos, 0.0f, 0.0f), 
				                       Quaternion.identity) as GameObject;
			}
		}

	}
}
