using UnityEngine;
using System.Collections;

public class LevelSave : MonoBehaviour {

	public bool debug = true;

	// Use this for initialization
	void Start () {
		if (debug) {
			PlayerPrefs.DeleteAll();
		}
		PlayerPrefs.SetString("Last Level", Application.loadedLevelName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
