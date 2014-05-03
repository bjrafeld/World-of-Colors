using UnityEngine;
using System.Collections;

public class DestroyOffScreen : MonoBehaviour {

	private WeightSpawn spawner;
	private TriggerSpawner tSpawner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBecameInvisible() {
		Vector3 top = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
		if(this.transform.position.y < top.y) {
			if(spawner != null) {
				spawner.weightDestroyed();
			} else {
				tSpawner.weightDestroyed();
			}
			Destroy(this.gameObject);
		}
	}

	public void SetSpawner(GameObject spawn) {
		spawner = spawn.GetComponent<WeightSpawn>();
		if (spawner == null) {
			tSpawner = spawn.GetComponent<TriggerSpawner>();
		}
	}
}
