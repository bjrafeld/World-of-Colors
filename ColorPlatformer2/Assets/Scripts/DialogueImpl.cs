using UnityEngine;
using System.Collections;

public class DialogueImpl : MonoBehaviour {

	public TextMesh[] lines;
	private GameObject _player;
	public GameObject buttonPrefab;
	public DialogueImpl nextLine = null;

	public GameObject portalSpawner;

	public float alphaChangeRate = 0.75f;
	public float buttonAlphaChangeRate = 1.25f;

	private bool fadeInComplete = false;
	private bool fadeInButtonComplete = false;
	private bool fadeOutComplete = false;
	private bool startAnimation = false;
	private bool fadeOutPressed = false;

	private GameObject aButton;
	public float timeBeforeButton = 0f;

	// Use this for initialization
	void Start () {
		aButton = GameObject.FindGameObjectWithTag("A_Button");
		if(aButton == null) {
			Debug.Log ("Could not find A Button Sprite in scene.");
		}
		_player = GameObject.FindGameObjectWithTag("Player");
		lines = GetComponentsInChildren<TextMesh>() as TextMesh[];
	}
	
	// Update is called once per frame
	void Update () {
		if(startAnimation) {
			Debug.Log ("Starting Animation of Dialogue");
			FadeIn ();
		} else if(fadeInComplete) {
			//FadeInButton ();
			if(Input.GetAxis("Jump") != 0 || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown (KeyCode.Space)) {
				fadeOutPressed = true;
				fadeInComplete = false;
			}
		} else if (fadeOutPressed) {
			FadeOut();
		} else if (fadeOutComplete) {
			Debug.Log ("Next line should start");
			if(nextLine == null) {
				_player.GetComponent<FreezeWeight>().UnFreeze();
				GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<LevelBeginAnimation>().RestoreGravity();
				if(portalSpawner != null) {
					portalSpawner.GetComponent<OpenPortal>().SpawnPortal();
				}
			} else {
				nextLine.StartAnim();
			}
			Destroy (this.gameObject);
		}
		
	}

	public void StartAnim() {
		startAnimation = true;
	}

	private void FadeIn() {
		bool innerFadeInComplete = false;
		foreach(TextMesh line in lines) {
			Color c = line.color;
			if(c.a < 1) {
				Color newAlpha = c;
				newAlpha.a = newAlpha.a + ((alphaChangeRate) * Time.deltaTime);
				line.color = newAlpha;
			} if(c.a >= 1) {
				Color newAlpha = c;
				newAlpha.a = 1;
				line.color = newAlpha;
				innerFadeInComplete = true;
			}
		}
		if(innerFadeInComplete) {
			startAnimation = false;
			fadeInComplete = true;
		}
	}

	private void FadeInButton() {
		bool fadeInButtonStart = false;
		if(timeBeforeButton <= 0) {
			fadeInButtonStart = true;
		} else {
			timeBeforeButton -= Time.deltaTime;
		}

		if (fadeInButtonStart) {
			Color c = aButton.GetComponent<SpriteRenderer>().color;
			c.a = c.a + ((buttonAlphaChangeRate) * Time.deltaTime);
			aButton.GetComponent<SpriteRenderer>().color = c;
		}
		if(aButton.GetComponent<SpriteRenderer>().color.a >= 1) {
			fadeInButtonComplete = true;
		}

	}

	private void FadeOut() {

		//Color buttonColor = aButton.GetComponent<SpriteRenderer>().color;
		//buttonColor.a = 0;

		//aButton.GetComponent<SpriteRenderer>().color = buttonColor;

		bool innerFadeOutComplete = false;
		foreach(TextMesh line in lines) {
			Color c = line.color;
			if(c.a > 0) {
				Color newAlpha = c;
				newAlpha.a = newAlpha.a - ((alphaChangeRate) * Time.deltaTime);
				line.color = newAlpha;
			} if(c.a <= 0) {
				Color newAlpha = c;
				newAlpha.a = 0;
				line.color = newAlpha;
				innerFadeOutComplete = true;
			}
		}
		if(innerFadeOutComplete) {
			Debug.Log ("Inner fade out compl");
			fadeOutPressed = false;
			fadeOutComplete = true;
		}
	}
}
