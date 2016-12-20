using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobSpawner : MonoBehaviour {

	public GameObject mobTemplate;
	public float spawnRange = 100f;
	public GameObject player;

	// Use this for initialization
	void Start () {
		if (mobTemplate == null || player == null)
			return;

		for (int mobCount = 0; mobCount < 10; mobCount++) {
			Transform offset = player.transform;
			offset.position = new Vector3 ((Random.value - 0.5f) * spawnRange, 0f, (Random.value - 0.5f) * spawnRange);
			offset.position = new Vector3 (offset.position.x, Terrain.activeTerrain.SampleHeight (offset.position), offset.position.z);
			Instantiate (mobTemplate, offset);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
