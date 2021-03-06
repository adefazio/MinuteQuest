﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterGenerator : MonoBehaviour {

	private Dictionary<int, bool> spawnedMonstersInSegment;

	private float segmentWidth = 30.0f;

	private System.Random rnd;
	private int gangsPerSegmentMin = 1;
	private int gangsPerSegmentMax = 4;
	private int monstersPerGangMin = 1;
	private int monstersPerGangMax = 4;
	private float gangRadius = 2.0f;

	private List<Object> monstersPrefabs = new List<Object>();
	private List<int> monsterSpawnRatios = new List<int>();

	private Attributes attrs;
	private LoadedDie samplingDistrubtion;

	// Use this for initialization
	void Start () {
		attrs = GetComponent<Attributes>();

		rnd = new System.Random();
		spawnedMonstersInSegment = new Dictionary<int, bool>();

		monstersPrefabs.Add(Resources.Load(
			"Monsters/GreenBlob", typeof(GameObject)));
		monsterSpawnRatios.Add(10);

		monstersPrefabs.Add(Resources.Load(
			"Monsters/BlueBlob", typeof(GameObject)));
		monsterSpawnRatios.Add(3);
		monstersPrefabs.Add(Resources.Load(
			"Monsters/YellowBlob", typeof(GameObject)));
		monsterSpawnRatios.Add(3);
		monstersPrefabs.Add(Resources.Load(
			"Monsters/RedBlob", typeof(GameObject)));
		monsterSpawnRatios.Add(4);
		monstersPrefabs.Add(Resources.Load(
			"Monsters/PurpleBlob", typeof(GameObject)));
		monsterSpawnRatios.Add(2);

		samplingDistrubtion = new LoadedDie(monsterSpawnRatios);
	}

	private int segment(Vector2 v) {
		return (int)(v.x / segmentWidth);
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
		return x + Random.value*(y-x);
	}

	void spawnMonstersInSegment(int s) {
		spawnedMonstersInSegment[s] = true;

		var ngangs = rnd.Next(gangsPerSegmentMin, gangsPerSegmentMax+1);

		for(int gang=0; gang < ngangs; gang++) {
			var nmonsters = rnd.Next(monstersPerGangMin, monstersPerGangMax+1);
			var gangPos = nextFloat(segmentWidth*s, segmentWidth*(s+1));

			// Don't spawn too close to player.
			if(Mathf.Abs(gangPos - transform.position.x) < 5.0f) {
				continue; 
			}

			for(int monster=0; monster < nmonsters; monster++) {
				var monsterPos = nextFloat(gangPos-gangRadius, gangPos+gangRadius);
				var monsterPrefab = monstersPrefabs[samplingDistrubtion.NextValue()];

				var mobj = Instantiate(monsterPrefab, new Vector3(monsterPos, 0.0f, 0.0f), 
				                       Quaternion.identity) as GameObject;
				var mattr = mobj.GetComponent<MonsterAttributes>();
				mattr.level = System.Math.Max(1, attrs.level + rnd.Next(-2, 3));
			}
		}

	}
}
