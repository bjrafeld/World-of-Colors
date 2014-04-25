using UnityEngine;
using System.Collections;

public class DestroyOffScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBecameInvisible() {
		Vector3 top = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
		Debug.Log ("I'm the invisible woman!");
		if(this.transform.position.y < top.y) {
			Debug.Log ("I'm the Invisible Woman, and I'm about to be destroyed!");
			Destroy(this.gameObject);
		}
	}
}
