using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public float trackSpeed = 10;

	private Transform target;

	public void SetTarget(Transform t) {
		target = t;
	}

	void LateUpdate() {
		if(target) {
			float x = IncrementTowards(transform.position.x, target.position.x, trackSpeed);
			float y = IncrementTowards(transform.position.y, target.position.y, trackSpeed);
			transform.position = new Vector3(x, y, transform.position.z);
		}
	}

	float IncrementTowards(float n, float target, float a) {
		if(n == target) {
			return n;
		} else {
			float dir = Mathf.Sign(target - n);
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign (target - n))? n: target;
		}
	}
}
