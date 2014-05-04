using UnityEngine;
using System.Collections;

public class OpenPortal : MonoBehaviour {
	
	public GameObject portalPrefab;
	
	public float topSpeedPortal;
	private float minSpeedPortal;
	public float max_Portal;
	
	private float min_Portal;
	
	public float playerMoveSpeed;

	public string nextLevel;
	
	private float min_Portal_X, min_Portal_Y;
	private float max_Portal_X, max_Portal_Y;
	
	private float normal_Portal_X, normal_Portal_Y;
	
	public float growSpeed, shrinkSpeed;
	public float rotationChangeSpeed;


	private GameObject portal;
	public bool animationStarted = false;
	
	//animation flags
	private bool portalDoneGrowing = false;
	private bool portalDoneShrinking = false;
	
	private RotatePortal _portal;

	
	public bool normalAnimation = true;
	
	// Use this for initialization
	void Awake () {
			
			portal = Instantiate(portalPrefab, this.transform.position, Quaternion.identity) as GameObject;
		portal.GetComponent<PortalTrigger>().nextLevel = this.nextLevel;
			this.minSpeedPortal = portal.GetComponent<RotatePortal>().rotationSpeed;
			
			min_Portal_X = 0.01f * portal.transform.localScale.x;
			min_Portal_Y = 0.01f *portal.transform.localScale.y;
		normal_Portal_X = portal.transform.localScale.x;
		normal_Portal_Y = portal.transform.localScale.y;

			max_Portal_X = max_Portal * portal.transform.localScale.x;
			max_Portal_Y = max_Portal * portal.transform.localScale.y;
			portal.transform.localScale = new Vector3(min_Portal_X, min_Portal_Y, 1);
		
	}
	
	void Start() {
		
	}

	public void SpawnPortal() {
		animationStarted = true;
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
		if( (portal.transform.localScale.x > max_Portal_X - .001 && portal.transform.localScale.x < max_Portal_X + .001)) {
			portalDoneGrowing = true;
			animationStarted = false;

			return;
		}
		portal.transform.localScale = Vector3.Lerp (portal.transform.localScale, new Vector3(max_Portal_X, max_Portal_Y, 1), Time.deltaTime*shrinkSpeed);
	}
	
	private void ShrinkPortal() {
		Debug.Log ("Shrinking Portal");
		if ((portal.transform.localScale.x > normal_Portal_X - .1 && portal.transform.localScale.x < normal_Portal_X + .1)) {
			portalDoneShrinking = true;
			portalDoneGrowing = false;
		}
		portal.transform.localScale = Vector3.Lerp (portal.transform.localScale, new Vector3(normal_Portal_X, normal_Portal_Y, 1), Time.deltaTime*growSpeed);
	}
	
	private void DestroyPortalUnfreezePlayer() {
		portalDoneShrinking = false;
		
	}
}
