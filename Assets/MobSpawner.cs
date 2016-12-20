using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {

	public GameObject mobTemplate;
	public float spawnRange = 25f;
	public int spawnCount = 10;
	public GameObject player;

	// Use this for initialization
	void Start () {
		if (mobTemplate != null && player != null) {
			for (int mobCount = 0; mobCount < spawnCount; mobCount++) {
				var newMob = Instantiate (mobTemplate);
				newMob.transform.position = this.transform.position;
				float newX = newMob.transform.position.x + (Random.value - 0.5f) * spawnRange;
				float newZ = newMob.transform.position.z + (Random.value - 0.5f) * spawnRange;
				float newY = Terrain.activeTerrain.SampleHeight (new Vector3 (newX, 0f, newZ));
				newMob.transform.position = new Vector3 (newX, newY, newZ);
				newMob.transform.SetParent (this.transform);
				newMob.SetActive (true);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
