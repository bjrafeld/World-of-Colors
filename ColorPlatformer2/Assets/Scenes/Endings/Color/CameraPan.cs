using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

	public GameObject endSpot;

	public float duration = 5f;
	private float elapsed = 0;

	public string nextLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MoveCrystal();
	
	}

	private void MoveCrystal() {
		elapsed += Time.deltaTime;
		if(elapsed >= duration) {
			this.transform.position = endSpot.transform.position;
			Application.LoadLevel (nextLevel);
		}

		this.transform.position = Vector3.Lerp (this.transform.position, endSpot.transform.position, (elapsed/duration * Time.deltaTime));
	}
}
