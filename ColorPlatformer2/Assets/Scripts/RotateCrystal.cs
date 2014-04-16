using UnityEngine;
using System.Collections;

public class RotateCrystal : MonoBehaviour {

	public float rotationSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, rotationSpeed*Time.deltaTime, Space.World);;
	}
}
