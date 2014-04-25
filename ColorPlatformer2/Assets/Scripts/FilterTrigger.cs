using UnityEngine;
using System.Collections;

public class FilterTrigger : MonoBehaviour {

	public FilterManager manager;

	public string colorTag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D col) {
		manager.SetColorFilterTag(colorTag);
	}
}
