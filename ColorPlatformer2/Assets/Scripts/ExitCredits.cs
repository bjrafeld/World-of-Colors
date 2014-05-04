using UnityEngine;
using System.Collections;

public class ExitCredits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetAxis("BackButton") != 0 || Input.GetButtonDown("Pause") || Input.GetButtonDown("Reset")) {
			Application.LoadLevel("MenuStart");
		}
	
	}
}
