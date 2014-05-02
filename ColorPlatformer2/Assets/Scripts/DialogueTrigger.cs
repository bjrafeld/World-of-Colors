using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour {

	public GameObject firstLine;
	private bool firstEntry = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(firstEntry) {
			if(col.tag == "Player") {
				col.gameObject.GetComponent<CharacterPhysics>().movementFrozen = true;
				firstLine.GetComponent<DialogueImpl>().StartAnim();
			}
			firstEntry = false;
		}
	}
}
