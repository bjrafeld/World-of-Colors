using UnityEngine;
using System.Collections;

public class MainMenuLayout : MonoBehaviour {
	
	public float centerSymmetry = 10f;
	public float heightBuffer = 10f;
	public float buttonHeight = 30f;
	public float buttonWidth = 100f;
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		float centerPointX = (Camera.main.pixelWidth/2 - buttonWidth);
		float centerPointY = (0 + buttonHeight);
		if(GUI.Button (new Rect((centerPointX - (1/2)*buttonWidth), centerPointY , buttonWidth, buttonHeight), "Level 1")) {
			Debug.Log ("Sandbox clicked");
			Application.LoadLevel("Level_1");
		}

		if(GUI.Button (new Rect((centerPointX - (1/2)*buttonWidth), centerPointY + heightBuffer, buttonWidth, buttonHeight), "Level 2")) {
			Debug.Log ("Sandbox clicked");
			Application.LoadLevel("Level_2");
		}

		if(GUI.Button (new Rect((centerPointX - (1/2)*buttonWidth), centerPointY + (2*heightBuffer), buttonWidth, buttonHeight), "Level 3")) {
			Debug.Log ("Sandbox clicked");
			Application.LoadLevel("Level_3");
		}

		if(GUI.Button (new Rect((centerPointX - (1/2)*buttonWidth), centerPointY + (3*heightBuffer), buttonWidth, buttonHeight), "Level 4")) {
			Debug.Log ("Sandbox clicked");
			Application.LoadLevel("Level_4");
		}
	}
}