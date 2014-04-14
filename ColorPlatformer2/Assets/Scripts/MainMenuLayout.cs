using UnityEngine;
using System.Collections;

public class MainMenuLayout : MonoBehaviour {
	
	public float centerSymmetry = 10f;
	public float heightBuffer = 0f;
	public float buttonHeight = 30f;
	public float buttonWidth = 100f;
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		float centerPointX = (Camera.main.pixelWidth/2);
		float centerPointY = (Camera.main.pixelHeight/2 - buttonHeight/2);
		if(GUI.Button (new Rect((centerPointX - (1/2)*buttonWidth), centerPointY + heightBuffer, buttonWidth, buttonHeight), "Sandbox")) {
			Debug.Log ("Sandbox clicked");
			Application.LoadLevel("sandbox");
		}
	}
}