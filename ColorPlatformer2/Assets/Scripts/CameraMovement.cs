using UnityEngine;
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

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		this.transform.position = new Vector3(player.transform.position.x + offsetStart_X, player.transform.position.y + offsetStart_Y, -10f);

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 bottomLeftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * edgeLeft, Screen.height * edgeBottom, player.transform.position.z));
		Vector3 topRightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * edgeRight, Screen.height * edgeTop, player.transform.position.z));

		Vector3 newCameraPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
		if(player.transform.position.x < bottomLeftBoundary.x) {
			newCameraPosition.x += player.transform.position.x - bottomLeftBoundary.x; 
		}
		if(player.transform.position.x > topRightBoundary.x) {
			newCameraPosition.x += player.transform.position.x - topRightBoundary.x;
		}
		if(player.transform.position.y < bottomLeftBoundary.y) {
			newCameraPosition.y += player.transform.position.y - bottomLeftBoundary.y;
		}
		if(player.transform.position.y > topRightBoundary.y) {
			newCameraPosition.y += player.transform.position.y - topRightBoundary.y;
		}

		Camera.main.transform.position = newCameraPosition;
	
	}
}
