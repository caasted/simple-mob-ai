using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehavior : MonoBehaviour {

	float randomTaskTimer;
	float randomTaskWait;
	Vector3 destination;
	float maxRange = float.PositiveInfinity;

	// Use this for initialization
	void Start () {
		randomTaskTimer = Time.time;
		randomTaskWait = Random.Range (5, 10);
		destination = this.transform.position;
		if (this.GetComponentInParent<MobSpawner> () != null) {
			maxRange = this.GetComponentInParent<MobSpawner> ().spawnRange;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - randomTaskTimer > randomTaskWait) {
			SetNewDestination ();
		}
		Vector3 heading = destination - this.transform.position;
		if (heading.magnitude > 0.1f) {
			this.transform.position = this.transform.position + (heading * Time.fixedDeltaTime * 0.5f);
		}
	}

	void SetNewDestination() {
		float newX = this.transform.position.x + (Random.value - 0.5f) * 10f;
		float newZ = this.transform.position.z + (Random.value - 0.5f) * 10f;
		float newY = Terrain.activeTerrain.SampleHeight (new Vector3 (newX, 0f, newZ)) + this.transform.localScale.y / 2f;
		destination = new Vector3 (newX, newY, newZ);
		if (this.transform.localPosition.magnitude > maxRange && this.GetComponentInParent<MobSpawner> () != null) { // Send home
			destination = this.GetComponentInParent<MobSpawner> ().transform.position;
		}
		randomTaskTimer = Time.time;
		randomTaskWait = Random.Range (5, 10);
	}
}
