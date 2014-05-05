using UnityEngine;
using System.Collections;

public class LevelBeginAnimation : MonoBehaviour {

	public GameObject portalPrefab;
	public GameObject playerPrefab;

	public float topSpeedPortal;
	private float minSpeedPortal;
	public float max_Portal;
	
	private float min_Portal;
	
	public float playerMoveSpeed;
	
	private float min_Portal_X, min_Portal_Y;
	private float max_Portal_X, max_Portal_Y;
	private float min_Player_X, min_Player_Y;
	
	public float growSpeed, shrinkSpeed;
	public float rotationChangeSpeed;
	
	public float playerShrinkSpeed;
	
	private GameObject player;
	private GameObject portal;
	private bool animationStarted = false;
	
	//animation flags
	private bool portalDoneGrowing = false;
	private bool portalDoneShrinking = false;
	
	private RotatePortal _portal;
	private string nextLevel;

	private float oldPlayerGravity;

	public bool normalAnimation = true;
	public bool startAnimation = false;

	private AudioClip portalClip;
	
	// Use this for initialization
	void Awake () {
		if(normalAnimation) {
			player = Instantiate(playerPrefab, this.transform.position, Quaternion.identity) as GameObject;
			min_Player_X = player.transform.localScale.x;
			min_Player_Y = player.transform.localScale.y;
			player.transform.localScale = new Vector3(min_Player_X, min_Player_Y, 1);
			player.GetComponent<CharacterPhysics>().movementFrozen = true;
			oldPlayerGravity = player.rigidbody2D.gravityScale;
			player.rigidbody2D.gravityScale = 0;

			portal = Instantiate(portalPrefab, this.transform.position, Quaternion.identity) as GameObject;
			portal.GetComponent<PortalTrigger>().buttonFade = false;
			this.minSpeedPortal = portal.GetComponent<RotatePortal>().rotationSpeed;

			min_Portal_X = min_Portal * portal.transform.localScale.x;
			min_Portal_Y = min_Portal * portal.transform.localScale.y;
			
			max_Portal_X = max_Portal * portal.transform.localScale.x;
			max_Portal_Y = max_Portal * portal.transform.localScale.y;
			portal.transform.localScale = new Vector3(min_Portal_X, min_Portal_Y, 1);


			animationStarted = true;
		} else if(startAnimation && normalAnimation) {
			player.rigidbody2D.gravityScale = 0.1f;
		} else {
			player = Instantiate(playerPrefab, this.transform.position, Quaternion.identity) as GameObject;
		}
		portalClip = Resources.Load ("portal") as AudioClip;
	}

	void Start() {
		if(normalAnimation) {
			portal.transform.position = player.transform.position;
			GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<AudioSource>().PlayOneShot(portalClip);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(animationStarted) {
			GrowPlayerAndPortal();
		} else if (portalDoneGrowing) {
			ShrinkPortal ();
		} else if (portalDoneShrinking) {
			DestroyPortalUnfreezePlayer();
		}
		
	}
	
	private void GrowPlayerAndPortal() {
		Debug.Log ("Starting animation and growing");
		if( (portal.transform.localScale.x > max_Portal_X - .001 && portal.transform.localScale.x < max_Portal_X + .001) && ( player.transform.localScale.x > min_Player_X - .001 && player.transform.localScale.x < min_Player_Y + .001)) {
			portalDoneGrowing = true;
			animationStarted = false;

			if(!startAnimation) {
				player.GetComponent<CharacterPhysics>().movementFrozen = false;
				player.rigidbody2D.gravityScale = oldPlayerGravity;
			} else {
				player.rigidbody2D.gravityScale = 0.1f;
			}
			return;
		}
		portal.transform.localScale = Vector3.Lerp (portal.transform.localScale, new Vector3(max_Portal_X, max_Portal_Y, 1), Time.deltaTime*shrinkSpeed);
		player.transform.localScale = Vector3.Lerp (player.transform.localScale, new Vector3(min_Player_X, min_Player_Y, 1), Time.deltaTime*shrinkSpeed);
	}

	private void ShrinkPortal() {
		Debug.Log ("Shrinking Portal");
		if ((portal.transform.localScale.x > min_Portal_X - .1 && portal.transform.localScale.x < min_Portal_X + .1)) {
			portalDoneShrinking = true;
			portalDoneGrowing = false;
		}
		portal.transform.localScale = Vector3.Lerp (portal.transform.localScale, new Vector3(min_Portal_X, min_Portal_Y, 1), Time.deltaTime*growSpeed);
	}
	
	private void DestroyPortalUnfreezePlayer() {
		Destroy (portal);
		portalDoneShrinking = false;

	}
	public void RestoreGravity() {
		player.GetComponent<CharacterPhysics>().movementFrozen = false;
		player.rigidbody2D.gravityScale = oldPlayerGravity;
	}
}
