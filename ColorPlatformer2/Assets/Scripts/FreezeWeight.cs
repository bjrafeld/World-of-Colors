using UnityEngine;
using System.Collections;

public class FreezeWeight : MonoBehaviour {

	private bool frozen = false;
	private Vector3 frozenPosition;
	private float oldGravity;

	// Use this for initialization
	void Start () {
		oldGravity = this.gameObject.rigidbody2D.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(frozen) {
			this.transform.position = frozenPosition;
		}
	}

	public void Freeze(Vector3 pos) {
		frozenPosition = pos;
		frozen = true;
		this.gameObject.rigidbody2D.gravityScale = 0;
	}

	public void UnFreeze() {
		frozen = false;
		this.gameObject.rigidbody2D.gravityScale = oldGravity;
	}
}
