using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour {

	public float rotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.back, rotationSpeed*Time.deltaTime, Space.Self);
	}
}
