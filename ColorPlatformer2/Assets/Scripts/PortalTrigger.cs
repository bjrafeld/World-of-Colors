using UnityEngine;
using System.Collections;

public class PortalTrigger : MonoBehaviour {

	private GameObject player;

	private bool portalTriggered = false;

	private GameObject xButton;
	public GameObject buttonPrefab;

	public float heightAbove = 1f;

	private float alphaChangeRate = 0.75f;

	private SpriteRenderer buttonColor;
	private RotatePortal _portal;
	private LevelEndAnimation _endAnim;

	public float topSpeedPortal = 20f;
	private float normalSpeedPortal;

	public string nextLevel;

	private bool animationNotStarted = true;

	public bool buttonFade = true;

	private AudioClip portalClip;

	// Use this for initialization
	void Start () {
		_portal = this.gameObject.GetComponent<RotatePortal>();
		_endAnim = this.gameObject.GetComponent<LevelEndAnimation>();
		this.normalSpeedPortal = _portal.rotationSpeed;
		portalClip = Resources.Load ("portal") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
		if(animationNotStarted) {
			if (portalTriggered) {
				if(buttonFade) {
					FadeInButton();
				}
			} else {
				if(buttonFade) {
					FadeOutButton();
				}
			}

			if(xButton != null) {
				if(Input.GetAxis("Red") != 0 || Input.GetKeyDown(KeyCode.X)) {
					animationNotStarted = false;
					Destroy (xButton);
					_endAnim.StartAnimation(player);
					GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<AudioSource>().PlayOneShot(portalClip);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			if(animationNotStarted) {
				_portal.setSpeedUp(topSpeedPortal);
			}
			player = col.gameObject;
			portalTriggered = true;
			if(xButton == null) {
				if(buttonFade) {
					CreateButton();
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag == "Player") {
			if(animationNotStarted) {
				_portal.setSlowDown(normalSpeedPortal);
			}
			portalTriggered = false;
		}
	}

	private void CreateButton() {
		if (xButton == null) {
			Vector3 position = player.transform.position;
			position.y += heightAbove;
			xButton = Instantiate(buttonPrefab, position, Quaternion.identity) as GameObject;
			buttonColor = xButton.GetComponent<SpriteRenderer>();
			Color zeroAlpha = buttonColor.color;
			zeroAlpha.a = 0;
			buttonColor.color = zeroAlpha;
		}
	}

	private void FadeInButton() {
		xButton.transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + heightAbove));
		if(buttonColor.color.a < 1) {
			Color newAlpha = buttonColor.color;
			newAlpha.a = newAlpha.a + ((alphaChangeRate) * Time.deltaTime);
			buttonColor.color = newAlpha;
		}

		if(buttonColor.color.a >= 1) {
			Color newAlpha = buttonColor.color;
			newAlpha.a = 1;
			buttonColor.color = newAlpha;
		}
	}

	private void FadeOutButton() {
		if(xButton != null) {
			xButton.transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + heightAbove));
			if(buttonColor.color.a <= 0) {
				player = null;
				Destroy(xButton);
			} else {
				Color newAlpha = buttonColor.color;
				newAlpha.a = newAlpha.a - ((2 * alphaChangeRate) * Time.deltaTime);
				buttonColor.color = newAlpha;
			}
		}
	}
}
