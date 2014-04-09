using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]

public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	[HideInInspector]
	public bool grounded;

	[HideInInspector]
	public bool movementStopped;

	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;

	public float skin = .005f;

	Ray ray;
	RaycastHit hit;

	void Start() {
		collider = GetComponent<BoxCollider>();
		size = collider.size;
		center = collider.center;

	}

	public void Move(Vector2 moveAmount) {

		float deltaX = moveAmount.x;
		float deltaY = moveAmount.y;
		Vector2 playerPosition = transform.position;

		grounded = false;

		for(int i = 0; i<3;i++) {
			float dir = Mathf.Sign(deltaY);
			float x = (playerPosition.x + center.x - size.x/2) + size.x/2 * i;
			float y = playerPosition.y + center.y + size.y/2 * dir;

			ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
			Debug.DrawRay(ray.origin, ray.direction);
			if(Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin, collisionMask)) {
				float distance = Vector3.Distance(ray.origin, hit.point);

				//Stop Player downward movement if comes within skin width of collider
				if(distance > skin) {
					deltaY = distance * dir - skin * dir;
				} else {
					deltaY = 0;
				}
				grounded = true;
				break;
			}

		}

		movementStopped = false;
		for(int i = 0; i<3;i++) {
			float dir = Mathf.Sign(deltaX);
			float x = playerPosition.x + center.x + size.x/2 * dir;
			float y = playerPosition.y + center.y - size.y/2 + size.y/2 * i;
			
			ray = new Ray(new Vector2(x, y), new Vector2(dir, 0));
			Debug.DrawRay(ray.origin, ray.direction);
			if(Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + skin, collisionMask)) {
				float distance = Vector3.Distance(ray.origin, hit.point);
				
				//Stop Player downward movement if comes within skin width of collider
				if(distance > skin) {
					deltaX = distance * dir - skin * dir;
				} else {
					deltaX = 0;
				}
				movementStopped = true;
				break;
			}
			
		}
		Vector2 finalTransform = new Vector2(deltaX, deltaY);

		transform.Translate(finalTransform);
	}
}
