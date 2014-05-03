using UnityEngine;
using System.Collections;

public class WeightSpawn : MonoBehaviour {

	//Script for the Spawner Empty Object

	public GameObject weightPrefab;
	private int numAlive = 0;
	public int maxAlive = 4;

	private GameObject weight;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (weight == null) {
			spawnWeight();
		}
	}

	public void spawnWeight() {
		if(numAlive < maxAlive) {
			weight = Instantiate(weightPrefab, transform.position, transform.rotation) as GameObject;
			weight.GetComponent<DestroyOffScreen>().SetSpawner(this.gameObject);
			numAlive++;
		}
	}

	public void weightDestroyed() {
		numAlive--;
	}


}
