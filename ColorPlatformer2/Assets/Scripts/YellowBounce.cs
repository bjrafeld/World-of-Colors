using UnityEngine;
using System.Collections;

public class YellowBounce : MonoBehaviour {

	public bool enabled = false;
	public float bounceForce = 5f;
	public enum bounceDirections {Up, Down, Right, Left};

	public bounceDirections bounce = bounceDirections.Up;

	private Vector3 directionBounce;


	// Use this for initialization
	void Start () {
		if(bounce == bounceDirections.Down) {
			directionBounce = Vector3.down;
		} else if (bounce == bounceDirections.Left) {
			directionBounce = Vector3.left;
		} else if (bounce == bounceDirections.Right) {
			directionBounce = Vector3.right;
		} else {
			directionBounce = Vector3.up;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(enabled && (col.gameObject.tag == "Player" || col.gameObject.tag == "weight")) {
			Debug.Log ("Adding jump force");
			col.gameObject.rigidbody2D.AddForce(bounceForce*directionBounce);
		}
	}
}
