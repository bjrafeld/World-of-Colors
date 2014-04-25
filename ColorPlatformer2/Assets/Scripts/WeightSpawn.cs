using UnityEngine;
using System.Collections;

public class WeightSpawn : MonoBehaviour {

	//Script for the Spawner Empty Object

	public GameObject weightPrefab;

	private GameObject weight;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (weight);
		if (weight == null) {
			spawnWeight();
		}
	}

	public void spawnWeight() {
		weight = Instantiate(weightPrefab, transform.position, transform.rotation) as GameObject;
	}
}
