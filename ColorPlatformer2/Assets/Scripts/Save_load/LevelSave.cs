using UnityEngine;
using System.Collections;

public class LevelSave : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString("Last Level", Application.loadedLevelName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
