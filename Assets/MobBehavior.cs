using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehavior : MonoBehaviour {

	public GameObject player;
	public bool aggressive = false;
	public float aggroRange = 10f;

	bool aggravated;
	float randomTaskTimer;
	float randomTaskWait;
	Vector3 destination;
	float maxRange = float.PositiveInfinity;

	// Use this for initialization
	void Start () {
		aggravated = false;
		randomTaskTimer = Time.time;
		randomTaskWait = Random.Range (5, 10);
		destination = this.transform.position;
		if (this.GetComponentInParent<MobSpawner> () != null) {
			maxRange = this.GetComponentInParent<MobSpawner> ().spawnRange;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (aggressive) {
			Vector3 aggroRangeCheck = player.transform.position - this.transform.position;
			if (aggroRangeCheck.magnitude < aggroRange) {
				aggravated = true;
			} else {
				aggravated = false;
			}
		}
		if (aggravated) {
			SetTarget ();
		} else {
			if (Time.time - randomTaskTimer > randomTaskWait) {
				SetNewDestination ();
			}
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

	void SetTarget() {
		float newX = player.transform.position.x;
		float newZ = player.transform.position.z;
		float newY = Terrain.activeTerrain.SampleHeight (new Vector3 (newX, 0f, newZ)) + this.transform.localScale.y / 2f;
		destination = new Vector3 (newX, newY, newZ);
		if (this.transform.localPosition.magnitude > maxRange && this.GetComponentInParent<MobSpawner> () != null) { // Send home
			aggravated = false;
			destination = this.GetComponentInParent<MobSpawner> ().transform.position;
			randomTaskTimer = Time.time;
			randomTaskWait = Random.Range (5, 10);
		}
	}
}
