using UnityEngine;
using System.Collections;

public class ParticlesReturn : MonoBehaviour {

	private GameObject _player;
	public float speed = 1.5f;

	public float lifetime = 4;

	private bool firstHit = true;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if(lifetime <= 0) {
			Destroy(this.gameObject);
		}
		Vector3 newVector = _player.transform.position - this.transform.position;
		this.rigidbody2D.AddForce(newVector * speed);
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log ("triggerd");
		if(col.tag == "Player") {
			if(firstHit) {
				firstHit = false;
			} else {
				Destroy (this.gameObject);
			}
		}
	}
}
