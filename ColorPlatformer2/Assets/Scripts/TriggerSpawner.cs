using UnityEngine;
using System.Collections;

public class TriggerSpawner : MonoBehaviour {
	
	//Script for the Spawner Empty Object
	
	public GameObject weightPrefab;
	
	private GameObject weight;
	
	public bool spawnOnStart = true;
	
	
	// Use this for initialization
	void Start () {
		if(spawnOnStart) {
			spawnWeight();
		}
	}
	
	// Update is called once per frame
	
	public void spawnWeight() {
		weight = Instantiate(weightPrefab, transform.position, transform.rotation) as GameObject;
	}
}
