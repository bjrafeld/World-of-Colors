using UnityEngine;
using System.Collections;

public class ReturnCrystalTrigger : MonoBehaviour {

	public GameObject buttonPrefab;
	public GameObject crystaEndSpot;
	public GameObject crystalPrefab;

	private GameObject crystal;
	private GameObject xButton;

	private bool triggered = false;
	private bool buttonCreated = false;
	private bool crystalCreated = false;
	private bool crystalDoneMoving = false;

	private GameObject player;

	public float heightAbove = 1f;

	public float duration = 2f;
	private float elapsed = 0;

	private bool firstPressed = true;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(firstPressed) {
			if(triggered) {
				FadeInButton();
			} else {
				FadeOutButton();
			}
		}

		if(buttonCreated) {
			if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Jump") != 0) {
				if(firstPressed) {
					crystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity) as GameObject;
					Destroy (xButton);
					firstPressed = false;
					crystalCreated = true;
					player.GetComponent<CharacterPhysics>().movementFrozen = true;
				}
			}
		}

		if(crystalCreated) {
			MoveCrystal();
		}

		if(crystalDoneMoving) {
			player.GetComponent<CharacterPhysics>().movementFrozen = false;
		}
	}

	private void FadeInButton() {
		xButton.transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + heightAbove));
		if(xButton.GetComponent<SpriteRenderer>().color.a < 1) {
			Color newAlpha = xButton.GetComponent<SpriteRenderer>().color;
			newAlpha.a = newAlpha.a + ((0.75f) * Time.deltaTime);
			xButton.GetComponent<SpriteRenderer>().color = newAlpha;
		}
		
		if(xButton.GetComponent<SpriteRenderer>().color.a >= 1) {
			Color newAlpha = xButton.GetComponent<SpriteRenderer>().color;
			newAlpha.a = 1;
			xButton.GetComponent<SpriteRenderer>().color = newAlpha;
		}
	}

	private void FadeOutButton() {
		if(xButton != null) {
			xButton.transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + heightAbove));
			if(xButton.GetComponent<SpriteRenderer>().color.a <= 0) {
				player = null;
				Destroy(xButton);
				buttonCreated = false;
			} else {
				Color newAlpha = xButton.GetComponent<SpriteRenderer>().color;
				newAlpha.a = newAlpha.a - ((2 * 0.75f) * Time.deltaTime);
				xButton.GetComponent<SpriteRenderer>().color = newAlpha;
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			triggered = true;
			CreateButton ();
		}
	}

	public void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "Player") {
			triggered = false;
		}
	}

	private void CreateButton() {
		if (xButton == null) {
			Vector3 position = player.transform.position;
			position.y += heightAbove;
			xButton = Instantiate(buttonPrefab, position, Quaternion.identity) as GameObject;
			Color zeroAlpha = xButton.GetComponent<SpriteRenderer>().color;
			zeroAlpha.a = 0;
			xButton.GetComponent<SpriteRenderer>().color = zeroAlpha;
			buttonCreated = true;
		}
	}

	private void MoveCrystal() {
		elapsed += Time.deltaTime;
		if(elapsed >= duration) {
			crystal.transform.position = crystaEndSpot.transform.position;
			crystal.GetComponent<CrystalBobbing>().Reset();
		}
		if(crystal.transform.position == crystaEndSpot.transform.position) {
			crystalDoneMoving = true;
			crystalCreated = false;
		}
		crystal.transform.position = Vector3.Lerp (crystal.transform.position, crystaEndSpot.transform.position, elapsed/duration);
	}

	public void TurnOn() {
		crystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity) as GameObject;
		crystalCreated = true;
	}


	
}
