using UnityEngine;
using System.Collections;

public class CrystalBobbing : MonoBehaviour {

	public float heightAbove;
	public float heightBelow;

	public float moveSpeed;

	public bool movingUp = true;

	private Vector3 topPoint, bottomPoint;
	private Vector3 bottomPointBuffer, topPointBuffer;

	// Use this for initialization
	void Start () {
		Vector3 pos = this.transform.position;
		bottomPoint = pos;
		bottomPoint.y -= heightBelow;
		
		topPoint = pos;
		topPoint.y += heightAbove;
		
		topPointBuffer = topPoint;
		topPointBuffer.y += .1f;
		bottomPointBuffer = bottomPoint;
		bottomPointBuffer.y -= .1f;
	}
	
	// Update is called once per frame
	void Update () {
		if(movingUp) {
			this.transform.position = Vector3.Lerp (this.transform.position, topPointBuffer, moveSpeed*Time.deltaTime);
			if(this.transform.position.y >= topPoint.y) {
				movingUp = false;
			}
		} else {
			this.transform.position = Vector3.Lerp (this.transform.position, bottomPointBuffer, moveSpeed*Time.deltaTime);
			if(this.transform.position.y <= bottomPoint.y) {
				movingUp = true;
			}
		}
	}

	public void Reset() {
		Vector3 pos = this.transform.position;
		bottomPoint = pos;
		bottomPoint.y -= heightBelow;
		
		topPoint = pos;
		topPoint.y += heightAbove;
		
		topPointBuffer = topPoint;
		topPointBuffer.y += .1f;
		bottomPointBuffer = bottomPoint;
		bottomPointBuffer.y -= .1f;
	}
}
