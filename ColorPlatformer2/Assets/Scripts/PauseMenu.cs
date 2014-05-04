using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject menuScreen;

	private GameObject screenObj;
	private bool paused;

	public GameObject backButtonPrefab;
	public GameObject menuButtonPrefab;

	public GameObject backButton;
	public GameObject menuButton;

	private float heightBetween = .7f;

	private int currentButton = 0;

	public Color selectedColor;
	private Color normalColor;

	private bool stickLock = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(paused) {
			if(Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetAxisRaw("VerticalJoy") == 1 && stickLock == false)) {
				Debug.Log ("Changing the menu button Up");
				stickLock = true;
				currentButton--;
			}
			
			if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetAxisRaw("VerticalJoy") == -1 && stickLock == false)) {
				Debug.Log ("Changing the menu button Down");
				stickLock = true;
				currentButton++;
			}
			
			if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Space) || Input.GetAxis ("Jump") != 0) {
				ActivateButton(currentButton);
			}
			
			if(stickLock && Input.GetAxisRaw("VerticalJoy") == 0) {
				stickLock = false;
			}
			if (currentButton == -1) {
				currentButton = 1;
			} else if (currentButton == 2) {
				currentButton = 0;
			}
			SetSelected(currentButton);
		}
	
	}
	

	public void Pause() {
		Time.timeScale = 0;
		paused = true;
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(.5f, .5f));
		pos.z = -1;
		screenObj = Instantiate(menuScreen, pos, menuScreen.transform.rotation) as GameObject;
		displayPauseMenu();
	}

	public void UnPause() {
		Destroy(backButton);
		Destroy (menuButton);
		Debug.Log ("Unpaused");
		Time.timeScale = 1;
		paused = false;
		Destroy (screenObj);
		currentButton = 0;
	}

	private void displayPauseMenu() {
		Vector3 topPos = Camera.main.transform.position;
		Vector3 bottomPos = Camera.main.transform.position;
		topPos.z = -2;
		bottomPos.z = -2;
		topPos.y += heightBetween;
		bottomPos.y -= heightBetween;

		topPos.y += 1;
		bottomPos.y += 1;

		backButton = Instantiate(menuButtonPrefab, topPos, Quaternion.identity) as GameObject;
		menuButton = Instantiate(backButtonPrefab, bottomPos, Quaternion.identity) as GameObject;

		normalColor = backButton.GetComponent<TextMesh>().color;
		SetSelected(currentButton);


	}

	public void SetSelected(int button) {
		if(button == 0) {
			backButton.GetComponent<TextMesh>().color = selectedColor;
			menuButton.GetComponent<TextMesh>().color = normalColor;
		} else if (button == 1) {
			backButton.GetComponent<TextMesh>().color = normalColor;
			menuButton.GetComponent<TextMesh>().color = selectedColor;
		}
	}
	
	public void ActivateButton(int button) {
		if(button == 0) {
			Application.LoadLevel("MenuStart");
		} else if (button == 1) {
			UnPause();
		}
	}
}
