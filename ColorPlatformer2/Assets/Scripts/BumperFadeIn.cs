using UnityEngine;
using System.Collections;

public class BumperFadeIn : MonoBehaviour {

	public GameObject bumperPrefab;
	private GameObject bumpers;

	private bool showBumpers = false;
	private bool buttonPressed = false;
	private bool fadeInComplete = false;
	private GameObject player;

	private SpriteRenderer buttonColor;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
		if(showBumpers) {
			FadeInBumpers();
		}
		if(fadeInComplete) {
			if(Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown("Yellow") || Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("Blue")) {
				showBumpers = false;
				Destroy(bumpers);
			}
		}
	}

	public void ShowBumpers() {
			Vector3 position = player.transform.position;
			position.y += 1f;
			bumpers = Instantiate(bumperPrefab, position, Quaternion.identity) as GameObject;
			buttonColor = bumpers.GetComponent<SpriteRenderer>();
			Color zeroAlpha = buttonColor.color;
			zeroAlpha.a = 0;
			buttonColor.color = zeroAlpha;
		showBumpers = true;
	}

	void FadeInBumpers() {
		bumpers.transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + 1f));
		if(buttonColor.color.a < 1) {
			Color newAlpha = buttonColor.color;
			newAlpha.a = newAlpha.a + ((1f) * Time.deltaTime);
			buttonColor.color = newAlpha;
		}
		
		if(buttonColor.color.a >= 1) {
			Color newAlpha = buttonColor.color;
			newAlpha.a = 1;
			buttonColor.color = newAlpha;
			fadeInComplete = true;
		}
	}
}
