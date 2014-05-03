using UnityEngine;
using System.Collections;

public class BlueGate : MonoBehaviour {

	public enum Direction {Up, Down, Left, Right};

	public Direction movementDirection = Direction.Down;
	public float movementAmount;
	public float movementSpeed;

	public GameObject affectedObject;
	public Direction affectedDirection;
	public float affectedMovementAmount;
	public float affectedMovementSpeed;

	private bool triggered = false;

	private Vector3 _bluePosition;
	private Vector3 _affectedPosition;

	private bool gate = false;

	// Use this for initialization
	void Awake () {
		if(affectedObject != null) {
			gate = true;
		}
		if(gate) {
			_bluePosition = this.transform.position;
			_affectedPosition = affectedObject.transform.position;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(gate) {
			if(triggered) {
				MoveSelf();
				MoveAffected();
			} else {
				Debug.Log ("Returning back..");
				ReturnSelf();
				ReturnAffected();
			}
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		if(col.gameObject.tag == "weight") {
			Debug.Log ("Triggered on");
			triggered = true;
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "weight") {
			Debug.Log ("Triggered off");
			triggered = false;
		}
	}

	public void TriggerOff() {
		triggered = false;
	}

	private void MoveSelf() {
		//Need to change if direction is not down
		if(this.transform.position.y > (_bluePosition.y - movementAmount)) {
			this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y) - (movementSpeed*Time.deltaTime));
		} else if (this.transform.position.y < (_bluePosition.y - movementAmount)) {
			this.transform.position = new Vector3(this.transform.position.x, _bluePosition.y - movementAmount);
		}
	}

	private void MoveAffected() {
		if (affectedDirection == Direction.Down) {
			if (affectedObject.transform.position.y > (_affectedPosition.y - affectedMovementAmount)) {
				affectedObject.transform.position = new Vector3(affectedObject.transform.position.x, (affectedObject.transform.position.y) - (affectedMovementSpeed*Time.deltaTime));
			} else if (affectedObject.transform.position.y <= (_affectedPosition.y - movementAmount)) {
				affectedObject.transform.position = new Vector3(affectedObject.transform.position.x, _affectedPosition.y - affectedMovementAmount);
			}
		} else if (affectedDirection == Direction.Up) {
			if (affectedObject.transform.position.y < (_affectedPosition.y + affectedMovementAmount)) {
				affectedObject.transform.position = new Vector3(affectedObject.transform.position.x, (affectedObject.transform.position.y) + (affectedMovementSpeed*Time.deltaTime));
			} else if (affectedObject.transform.position.y >= (_affectedPosition.y + affectedMovementAmount)) {
				affectedObject.transform.position = new Vector3(affectedObject.transform.position.x, _affectedPosition.y + affectedMovementAmount);
			}
		} else if (affectedDirection == Direction.Left) {
			if (affectedObject.transform.position.x > (_affectedPosition.x - affectedMovementAmount)) {
				affectedObject.transform.position = new Vector3((affectedObject.transform.position.x) - (affectedMovementSpeed*Time.deltaTime), affectedObject.transform.position.y);
			} else if (affectedObject.transform.position.x <= (_affectedPosition.x - movementAmount)) {
				affectedObject.transform.position = new Vector3(_affectedPosition.x - affectedMovementAmount, affectedObject.transform.position.y);
			}
		} else if (affectedDirection == Direction.Right) {
			if (affectedObject.transform.position.x < (_affectedPosition.x + affectedMovementAmount)) {
				affectedObject.transform.position = new Vector3((affectedObject.transform.position.x) + (affectedMovementSpeed*Time.deltaTime), affectedObject.transform.position.y );
			} else if (affectedObject.transform.position.x >= (_affectedPosition.x + affectedMovementAmount)) {
				affectedObject.transform.position = new Vector3(_affectedPosition.x + affectedMovementAmount, affectedObject.transform.position.y);
			}
		}

		if(affectedObject.GetComponent<ConstantDown>() != null) {
			affectedObject.GetComponent<ConstantDown>().triggerOff();
		}
	}

	private void ReturnSelf() {
		if(this.transform.position.y <_bluePosition.y) {
			this.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y) + (movementSpeed*Time.deltaTime));
		} else if (this.transform.position.y > _bluePosition.y) {
			this.transform.position = new Vector3(this.transform.position.x, _bluePosition.y);
		}
	}

	private void ReturnAffected() {
		if (affectedDirection == Direction.Up) {
			if (affectedObject.transform.position.y > (_affectedPosition.y)) {
				affectedObject.transform.position = new Vector3(affectedObject.transform.position.x, (affectedObject.transform.position.y) - (affectedMovementSpeed*Time.deltaTime));
			} else if (affectedObject.transform.position.y <= (_affectedPosition.y)) {
				affectedObject.transform.position = new Vector3(affectedObject.transform.position.x, _affectedPosition.y);
			}
		} else if (affectedDirection == Direction.Down) {
			if (affectedObject.transform.position.y < (_affectedPosition.y)) {
				affectedObject.transform.position = new Vector3(affectedObject.transform.position.x, (affectedObject.transform.position.y) + (affectedMovementSpeed*Time.deltaTime));
			} else if (affectedObject.transform.position.y >= (_affectedPosition.y)) {
				affectedObject.transform.position = new Vector3(affectedObject.transform.position.x, _affectedPosition.y);
			}
		} else if (affectedDirection == Direction.Right) {
			if (affectedObject.transform.position.x > (_affectedPosition.x)) {
				affectedObject.transform.position = new Vector3((affectedObject.transform.position.x) - (affectedMovementSpeed*Time.deltaTime), affectedObject.transform.position.y);
			} else if (affectedObject.transform.position.x <= (_affectedPosition.x)) {
				affectedObject.transform.position = new Vector3(_affectedPosition.x, affectedObject.transform.position.y);
			}
		} else if (affectedDirection == Direction.Left) {
			if (affectedObject.transform.position.x < (_affectedPosition.x)) {
				affectedObject.transform.position = new Vector3((affectedObject.transform.position.x) + (affectedMovementSpeed*Time.deltaTime), affectedObject.transform.position.y );
			} else if (affectedObject.transform.position.x >= (_affectedPosition.x)) {
				affectedObject.transform.position = new Vector3(_affectedPosition.x, affectedObject.transform.position.y);
			}
		}
	}
}
