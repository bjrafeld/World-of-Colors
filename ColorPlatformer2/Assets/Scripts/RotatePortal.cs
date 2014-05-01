using UnityEngine;
using System.Collections;

public class RotatePortal : MonoBehaviour {

	public float rotationSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.back, rotationSpeed*Time.deltaTime, Space.Self);
	}
}
