using UnityEngine;
using System.Collections;

public class FreezeOnRed : MonoBehaviour {

	private CharacterPhysics _player;

	// Use this for initialization	
	void Start () {
		Debug.Log (GameObject.FindGameObjectsWithTag("Player"));
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterPhysics>() as CharacterPhysics;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		Debug.Log ("Collided with " + col.gameObject.tag);
		if(col.gameObject.tag == "Player") {
			_player.movementFrozen = true;
		}

		if(col.gameObject.tag == "weight") {
			//Do swomething to the weight
		}
	}
}
