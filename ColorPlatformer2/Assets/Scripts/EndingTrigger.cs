using UnityEngine;
using System.Collections;

public class EndingTrigger : MonoBehaviour {

	private GameObject xButton;
	public GameObject buttonPrefab;
	private SpriteRenderer buttonColor;
	public GameObject crystaEndSpot;

	public GameObject crystalPrefab;
	private GameObject crystal;
	private bool crystalCreated = false;

	private float alphaChangeRate = 0.75f;
	public float heightAbove = 1f;

	private bool triggered = false;

	private GameObject player;

	private bool buttonFade = true;

	public float duration = 2f;
	private float elapsed = 0;

	public string finalEnding;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered) {
			if(buttonFade) {
				FadeInButton();
			}
			if(xButton != null) {
				if(Input.GetAxis("Red") != 0 || Input.GetKeyDown(KeyCode.X)) {
					Destroy (xButton);
					buttonFade = false;
					crystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity) as GameObject;
					crystalCreated = true;
					player.GetComponent<CharacterPhysics>().movementFrozen = true;
				}
			}
		} else {
			FadeOutButton();
		}

		if(crystalCreated) {
			MoveCrystal();
		}


	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			player = col.gameObject;
			triggered = true;
			if(xButton == null) {
					CreateButton();

			}
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			triggered = false;
		}
	}

	private void CreateButton() {
		if (xButton == null) {
			Vector3 position = player.transform.position;
			position.y += heightAbove;
			xButton = Instantiate(buttonPrefab, position, Quaternion.identity) as GameObject;
			buttonColor = xButton.GetComponent<SpriteRenderer>();
			Color zeroAlpha = buttonColor.color;
			zeroAlpha.a = 0;
			buttonColor.color = zeroAlpha;
		}
	}
	
	private void FadeInButton() {
		xButton.transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + heightAbove));
		if(buttonColor.color.a < 1) {
			Color newAlpha = buttonColor.color;
			newAlpha.a = newAlpha.a + ((alphaChangeRate) * Time.deltaTime);
			buttonColor.color = newAlpha;
		}
		
		if(buttonColor.color.a >= 1) {
			Color newAlpha = buttonColor.color;
			newAlpha.a = 1;
			buttonColor.color = newAlpha;
		}
	}
	
	private void FadeOutButton() {
		if(xButton != null) {
			xButton.transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + heightAbove));
			if(buttonColor.color.a <= 0) {
				player = null;
				Destroy(xButton);
			} else {
				Color newAlpha = buttonColor.color;
				newAlpha.a = newAlpha.a - ((2 * alphaChangeRate) * Time.deltaTime);
				buttonColor.color = newAlpha;
			}
		}
	}

	private void MoveCrystal() {
		elapsed += Time.deltaTime;
		if(elapsed >= duration) {
			crystal.transform.position = crystaEndSpot.transform.position;
			crystal.GetComponent<CrystalBobbing>().Reset();
		}
		if(crystal.transform.position == crystaEndSpot.transform.position) {
			Application.LoadLevel (finalEnding);
		}
		crystal.transform.position = Vector3.Lerp (crystal.transform.position, crystaEndSpot.transform.position, (elapsed/duration * Time.deltaTime));
	}


}
