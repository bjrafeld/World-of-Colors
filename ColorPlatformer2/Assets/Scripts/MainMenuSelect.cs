using UnityEngine;
using System.Collections;

public class MainMenuSelect : MonoBehaviour {

	private int currentButton = 0;
	TextMesh newGame;
	TextMesh continueButton;
	TextMesh credits;
	TextMesh quit;

	public Color selectedColor;
	private Color normalColor;

	private bool stickLock = false;
	private bool inputAllowed = true;
	private bool fadeDone = false;
	private bool buttonPressed = false;

	// Use this for initialization
	void Start () {
		newGame = GameObject.FindGameObjectWithTag("NewGame").GetComponent<TextMesh>();
		continueButton = GameObject.FindGameObjectWithTag("Continue").GetComponent<TextMesh>();
		credits = GameObject.FindGameObjectWithTag("Credits").GetComponent<TextMesh>();
		quit = GameObject.FindGameObjectWithTag("QuitGame").GetComponent<TextMesh>();

		normalColor = newGame.color;

		SetSelected(currentButton);

		stickLock = false;
		inputAllowed = true;
		fadeDone = false;
		buttonPressed = false;


	}
	
	// Update is called once per frame
	void Update () {
		if(inputAllowed) {
			if(Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetAxisRaw("VerticalJoy") == 1 && stickLock == false)) {
				Debug.Log ("Changing the menu button Up");
				stickLock = true;
				currentButton--;
				SetSelected(currentButton);
			}

			if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetAxisRaw("VerticalJoy") == -1 && stickLock == false)) {
				Debug.Log ("Changing the menu button Down");
				stickLock = true;
				currentButton++;
				SetSelected(currentButton);
			}

			if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown ("Jump")) {
				buttonPressed = true;
				inputAllowed = false;
			}
		}

		if(stickLock && Input.GetAxisRaw("VerticalJoy") == 0) {
			stickLock = false;
		}
		if (currentButton == -1) {
			currentButton = 3;
			SetSelected(currentButton);
		} else if (currentButton == 4) {
			currentButton = 0;
			SetSelected(currentButton);
		}

		if(buttonPressed) {
			FadeButton(currentButton);
		}

		if(fadeDone) {
			ActivateButton(currentButton);
		}
		                           
	
	}

	public void SetSelected(int button) {
		if(button == 0) {
			newGame.color = selectedColor;
			continueButton.color = normalColor;
			credits.color = normalColor;
			quit.color = normalColor;
		} else if (button == 1) {
			newGame.color = normalColor;
			continueButton.color = selectedColor;
			credits.color = normalColor;
			quit.color = normalColor;
		} else if (button == 2) {
			newGame.color = normalColor;
			continueButton.color = normalColor;
			credits.color = selectedColor;
			quit.color = normalColor;
		} else if (button == 3) {
			newGame.color = normalColor;
			continueButton.color = normalColor;
			credits.color = normalColor;
			quit.color = selectedColor;
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
		} else if ( button == 3) {
			Application.Quit();
		}
	}

	private void FadeButton(int button) {
		
		//Color buttonColor = aButton.GetComponent<SpriteRenderer>().color;
		//buttonColor.a = 0;
		
		//aButton.GetComponent<SpriteRenderer>().color = buttonColor;
		Debug.Log ("Fading out buttons");
		if(button != 0) {
			Color newAlpha = newGame.color;
			Debug.Log ("Alpha of new game changing");
			if(newAlpha.a > 0) {
				Debug.Log ("new game alpha going down");
				newAlpha.a = newAlpha.a - ((0.4f) * Time.deltaTime);
				newGame.color = newAlpha;
				Debug.Log(newGame.color);
			} else {
				newAlpha.a = 0;
				newGame.color = newAlpha;
				fadeDone = true;
			}
		} 
		if (button != 1) {
			Color newAlpha = continueButton.color;
			if(newAlpha.a > 0) {
				newAlpha.a = newAlpha.a - ((0.4f) * Time.deltaTime);
				continueButton.color = newAlpha;
			} else {
				newAlpha.a = 0;
				continueButton.color = newAlpha;
				fadeDone = true;
			}
		} 
		if(button != 2) {
			Color newAlpha = credits.color;
			if(newAlpha.a > 0) {
				newAlpha.a = newAlpha.a - ((0.4f) * Time.deltaTime);
				credits.color = newAlpha;
			} else {
				newAlpha.a = 0;
				credits.color = newAlpha;
				fadeDone = true;
			}
		} 
		if (button != 3) {
			Color newAlpha = quit.color;
			if(newAlpha.a > 0) {
				newAlpha.a = newAlpha.a - ((0.4f) * Time.deltaTime);
				quit.color = newAlpha;
			} else {
				newAlpha.a = 0;
				quit.color = newAlpha;
				fadeDone = true;
			}
		}

	}
}
