using UnityEngine;
using System.Collections;

public class LevelEndTrigger : MonoBehaviour {

	public string nextLevel = "main_menu";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCollisionEnter2D(Collision2D col) {
		if(col.transform.tag == "Player") {
			Application.LoadLevel(nextLevel);
		}
	}
}
