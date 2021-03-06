﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform spawnPoint;

	public GameObject playerPrefab;

	public float offsetStart_X;
	public float offsetStart_Y;

	public float edgeLeft = 0f;
	public float edgeRight = 1f;
	public float edgeBottom = 0f;
	public float edgeTop = 1f;

	public float maxY, minY;
	public float maxX, minX;

	public bool cameraMove;

	public bool resetOnKill = false;

	private GameObject player;

	private float _player_size;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		_player_size = player.transform.localScale.x/2;

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 bottomLeftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * edgeLeft, Screen.height * edgeBottom, player.transform.position.z));
		Vector3 topRightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * edgeRight, Screen.height * edgeTop, player.transform.position.z));

		Vector3 bottomLeftScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, player.transform.position.z));
		Vector3 topRightScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width , Screen.height, player.transform.position.z));

		Vector3 newCameraPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
		if(cameraMove) {
			bool bounded = false;

			if(player.transform.position.x < bottomLeftBoundary.x) {
				if ((newCameraPosition.x + player.transform.position.x - bottomLeftBoundary.x) >= minX) {
					newCameraPosition.x += player.transform.position.x - bottomLeftBoundary.x; 
				}
			}
			if(player.transform.position.x > topRightBoundary.x) {
				if ((newCameraPosition.x + player.transform.position.x - topRightBoundary.x) <= maxX) {
					newCameraPosition.x += player.transform.position.x - topRightBoundary.x;
				}
			}
			if(player.transform.position.y < bottomLeftBoundary.y) {
				if((newCameraPosition.y + player.transform.position.y - bottomLeftBoundary.y) >= minY) {
					newCameraPosition.y += player.transform.position.y - bottomLeftBoundary.y;
				}
			}
			if(player.transform.position.y > topRightBoundary.y) {
				if((newCameraPosition.y + player.transform.position.y - topRightBoundary.y) <= maxY) {
					newCameraPosition.y += player.transform.position.y - topRightBoundary.y;
				}
			}

			if(player.transform.position.x < (bottomLeftScreen.x + _player_size)) {
				player.transform.position = new Vector3((bottomLeftScreen.x + _player_size), player.transform.position.y, player.transform.position.z);
			}
			if(player.transform.position.x > (topRightScreen.x - _player_size)) {
				player.transform.position = new Vector3((topRightScreen.x - _player_size), player.transform.position.y, player.transform.position.z);
			}
			
			if (player.transform.position.y < (bottomLeftScreen.y - 5)) {
				if(resetOnKill) {
					Application.LoadLevel(Application.loadedLevel);
				} else {
					Destroy(player);
					player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
				}
			}
		

		} else {
			if(player.transform.position.x < (bottomLeftBoundary.x + _player_size)) {
				player.transform.position = new Vector3((bottomLeftBoundary.x + _player_size), player.transform.position.y, player.transform.position.z);
			}
			if(player.transform.position.x > (topRightBoundary.x - _player_size)) {
				player.transform.position = new Vector3((topRightBoundary.x - _player_size), player.transform.position.y, player.transform.position.z);
			}

			if (player.transform.position.y < (bottomLeftBoundary.y - 5)) {
				if(resetOnKill) {
					Application.LoadLevel(Application.loadedLevel);
				} else {
					Destroy(player);
					player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
				}
			}
		}
		//Check for corner boundaries

		Camera.main.transform.position = newCameraPosition;
	
	}
}
