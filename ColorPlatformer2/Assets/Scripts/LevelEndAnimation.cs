using UnityEngine;
using System.Collections;

public class LevelEndAnimation : MonoBehaviour {

	public float topSpeedPortal;
	public float max_Portal;

	public float min_Portal;
	public float min_Player;

	public float playerMoveSpeed;

	private float min_Portal_X, min_Portal_Y;
	private float max_Portal_X, max_Portal_Y;
	private float min_Player_X, min_Player_Y;

	public float growSpeed, shrinkSpeed;
	public float rotationChangeSpeed;

	public float playerShrinkSpeed;

	private GameObject player;
	private bool animationStarted = false;

	//animation flags
	private bool portalDoneGrowing = false;
	private bool portalDoneShrinking = false;

	private RotatePortal _portal;
	private string nextLevel;

	// Use this for initialization
	void Start () {
		_portal = this.gameObject.GetComponent<RotatePortal>();
		nextLevel = this.gameObject.GetComponent<PortalTrigger>().nextLevel;

		min_Portal_X = min_Portal * this.transform.localScale.x;
		min_Portal_Y = min_Portal * this.transform.localScale.y;

		max_Portal_X = max_Portal * this.transform.localScale.x;
		max_Portal_Y = max_Portal * this.transform.localScale.y;

	}
	
	// Update is called once per frame
	void Update () {
		if(animationStarted) {
			GrowPortal();
		} else if (portalDoneGrowing) {
			player.rigidbody2D.gravityScale = 0;
			ShrinkPlayerAndPortal();
		} else if (portalDoneShrinking) {
			DestroyAndLoadNext();
		}
	
	}

	public void StartAnimation(GameObject player) {
		this.player = player;
		this.player.GetComponent<CharacterPhysics>().movementFrozen = true;

		this.min_Player_X = min_Player * player.transform.localScale.x;
		this.min_Player_Y = min_Player * player.transform.localScale.y;

		animationStarted = true;
	}

	private void GrowPortal() {
		if (_portal.rotationSpeed == topSpeedPortal && (this.transform.localScale.x > max_Portal_X - .1 && this.transform.localScale.x < max_Portal_X + .1)) {
			portalDoneGrowing = true;
			animationStarted = false;
		}
		_portal.setSpeedUp(topSpeedPortal);
		this.transform.localScale = Vector3.Lerp (this.transform.localScale, new Vector3(max_Portal_X, max_Portal_Y, 1), Time.deltaTime*growSpeed);
	}

	private void ShrinkPlayerAndPortal() {
		if( (this.transform.localScale.x > min_Portal_X - .001 && this.transform.localScale.x < min_Portal_X + .001) && ( player.transform.localScale.x > min_Player_X - .001 && player.transform.localScale.x < min_Player_X + .001)) {
			portalDoneShrinking = true;
			portalDoneGrowing = false;
			return;
		}
		this.transform.localScale = Vector3.Lerp (this.transform.localScale, new Vector3(min_Portal_X, min_Portal_Y, 1), Time.deltaTime*shrinkSpeed);
		player.transform.localScale = Vector3.Lerp (player.transform.localScale, new Vector3(min_Player_X, min_Player_Y, 1), Time.deltaTime*shrinkSpeed);
		player.transform.position = Vector3.Lerp (player.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, 0), Time.deltaTime*playerMoveSpeed);
	}

	private void DestroyAndLoadNext() {
		Destroy (player);
		Application.LoadLevel(nextLevel);
		Destroy (this.gameObject);
	}
}
