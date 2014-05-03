using UnityEngine;
using System.Collections;

public class CrystalSave : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt(Application.loadedLevelName + "_crystal") == 1) {
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
