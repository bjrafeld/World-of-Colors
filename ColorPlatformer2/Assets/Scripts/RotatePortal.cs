using UnityEngine;
using System.Collections;

public class RotatePortal : MonoBehaviour {

	public float rotationSpeed = 1f;

	private float targetSpeed;

	public float speedUpDuration = 3f;

	public float incrementSpeed;
	private bool speedUp = false;

	// Use this for initialization
	void Start () {
		targetSpeed = rotationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if(speedUp) {
			if(this.targetSpeed > rotationSpeed) {
				rotationSpeed += incrementSpeed;
			} else if (rotationSpeed > targetSpeed) {
				rotationSpeed = targetSpeed;
			}
		} else {
			if(this.targetSpeed < rotationSpeed) {
				rotationSpeed -= (2*incrementSpeed);
			} else if(rotationSpeed < targetSpeed) {
				rotationSpeed = targetSpeed;
			}
		}
		transform.Rotate(Vector3.back, rotationSpeed*Time.deltaTime, Space.Self);
	}

	public void setSpeedUp(float targetSpeed) {
		Debug.Log ("Speed set up to: "+targetSpeed);
		this.targetSpeed = targetSpeed;
		this.speedUp = true;
	}

	public void setSlowDown(float targetSpeed) {
		this.targetSpeed = targetSpeed;
		this.speedUp = false;
	}
}
