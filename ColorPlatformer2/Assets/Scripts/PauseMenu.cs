using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject menuScreen;

	private GameObject screenObj;
	private bool paused;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if(paused) {
			displayPauseMenu();
		}
	}

	public void Pause() {
		Time.timeScale = 0;
		paused = true;
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(.5f, .5f));
		pos.z = -1;
		Quaternion rot = Quaternion.identity;
		rot.x = 90;
		screenObj = Instantiate(menuScreen, pos, menuScreen.transform.rotation) as GameObject;
	}

	public void UnPause() {
		Debug.Log ("Unpaused");
		Time.timeScale = 1;
		paused = false;
		Destroy (screenObj);
	}

	private void displayPauseMenu() {
		float centerPointX = (Camera.main.pixelWidth/2 - 50);
		float centerPointY = (0 + 50);
		if(GUI.Button (new Rect((centerPointX - ((1/2)*50)), centerPointY , 50, 50), "Main Menu")) {
			Debug.Log ("Sandbox clicked");
			Application.LoadLevel("Main_menu");
		}
	}
}
