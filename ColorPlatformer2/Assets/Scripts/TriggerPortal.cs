using UnityEngine;
using System.Collections;

public class TriggerPortal : MonoBehaviour {

	public OpenPortal portal;
	public float duration = .5f;

	private bool spawn = false;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			
			portal.SpawnPortal();
		}
	}
}
