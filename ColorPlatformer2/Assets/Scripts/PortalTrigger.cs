﻿using UnityEngine;
using System.Collections;

public class PortalTrigger : MonoBehaviour {

	private GameObject player;

	private bool portalTriggered = false;

	private GameObject xButton;
	public GameObject buttonPrefab;

	public float heightAbove = 1f;

	private float alphaChangeRate = 0.75f;

	private SpriteRenderer buttonColor;

	public string nextLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (portalTriggered) {
			FadeInButton();
			if(Input.GetAxis("Red") != 0 || Input.GetKeyDown(KeyCode.X)) {
				//Exit Animation
				Application.LoadLevel(nextLevel);
			}

		} else {
			FadeOutButton();
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			player = col.gameObject;
			portalTriggered = true;
			if(xButton == null) {
				CreateButton();
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			portalTriggered = false;
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
				newAlpha.a = newAlpha.a - (( alphaChangeRate) * Time.deltaTime);
				buttonColor.color = newAlpha;
			}
		}
	}
}
