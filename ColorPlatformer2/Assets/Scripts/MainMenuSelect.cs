using UnityEngine;
using System.Collections;

public class MainMenuSelect : MonoBehaviour {

	private int currentButton = 0;
	TextMesh newGame;
	TextMesh continueButton;
	TextMesh credits;

	public Color selectedColor;
	private Color normalColor;

	private bool stickLock = false;

	// Use this for initialization
	void Start () {
		newGame = GameObject.FindGameObjectWithTag("NewGame").GetComponent<TextMesh>();
		continueButton = GameObject.FindGameObjectWithTag("Continue").GetComponent<TextMesh>();;
		credits = GameObject.FindGameObjectWithTag("Credits").GetComponent<TextMesh>();;

		normalColor = newGame.color;

		SetSelected(currentButton);


	}
	
	// Update is called once per frame
	void Update () {
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

		if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown ("Jump")) {
			ActivateButton(currentButton);
		}

		if(stickLock && Input.GetAxisRaw("VerticalJoy") == 0) {
			stickLock = false;
		}
		if (currentButton == -1) {
			currentButton = 2;
		} else if (currentButton == 3) {
			currentButton = 0;
		}
		SetSelected(currentButton);
		                           
	
	}

	public void SetSelected(int button) {
		if(button == 0) {
			newGame.color = selectedColor;
			continueButton.color = normalColor;
			credits.color = normalColor;
		} else if (button == 1) {
			newGame.color = normalColor;
			continueButton.color = selectedColor;
			credits.color = normalColor;
		} else if (button == 2) {
			newGame.color = normalColor;
			continueButton.color = normalColor;
			credits.color = selectedColor;
		}
	}

	public void ActivateButton(int button) {
		if(button == 0) {
			Application.LoadLevel("TEMPLE_BEGINNING");
		} else if (button == 1) {
			string level = PlayerPrefs.GetString("Last Level");
			if(level == "") {
				Application.LoadLevel("TEMPLE_BEGINNING");
			} else {
				Application.LoadLevel(level);
			}
		} else if ( button == 2) {
			Application.LoadLevel("CREDITS");
		}
	}
}
