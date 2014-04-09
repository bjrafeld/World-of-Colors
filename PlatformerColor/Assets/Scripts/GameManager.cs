using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	private GameCamera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponent<GameCamera>();
		SpawnPlayer();
	}
	
	private void SpawnPlayer() {
		camera.SetTarget ((Instantiate(player, Vector3.zero, Quaternion.identity) as GameObject).transform);
	}
}
