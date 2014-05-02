using UnityEngine;
using System.Collections;

public class DialogueImpl : MonoBehaviour {

	public TextMesh[] lines;
	private GameObject _player;
	public GameObject buttonPrefab;
	public DialogueImpl nextLine = null;

	public float alphaChangeRate = 0.75f;

	private bool fadeInComplete = false;
	private bool fadeInButtonComplete = false;
	private bool fadeOutComplete = false;
	private bool startAnimation = false;
	private bool fadeOutPressed = false;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
		lines = GetComponentsInChildren<TextMesh>() as TextMesh[];
	}
	
	// Update is called once per frame
	void Update () {
		if(startAnimation) {
			Debug.Log ("Starting Animation of Dialogue");
			FadeIn ();
		} else if(fadeInComplete) {
			FadeInButton ();
		}else if (fadeInButtonComplete) {
			if(Input.GetAxis("Jump") != 0 || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown (KeyCode.Space)) {
				fadeOutPressed = true;
				fadeInButtonComplete = false;
			}
		} else if (fadeOutPressed) {
			FadeOut();
		} else if (fadeOutComplete) {
			Debug.Log ("Next line should start");
			if(nextLine == null) {
				_player.GetComponent<CharacterPhysics>().movementFrozen = false;
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
				//do stuff
		fadeInButtonComplete = true;
		fadeInComplete = false;
	}

	private void FadeOut() {

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
