using UnityEngine;
using System.Collections;

public class CrystalSpawnAndCollect : MonoBehaviour {

	public GameObject shardParticles;
	public int particleCount = 20;
	public float explosionForce;

	private AudioClip crystalClip;

	// Use this for initialization
	void Start () {
		crystalClip = Resources.Load ("crystal") as AudioClip;
		//Check if it's already been collected
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		//Save that's it has been collected
		//Save the total count of crystals
		if(col.tag == "Player") {
			GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<AudioSource>().PlayOneShot(crystalClip);
			for(int i = 0; i < particleCount; i++) {
				Quaternion rotation = Random.rotation;
				rotation.x = 0;
				rotation.y = 0;
				GameObject particle = Instantiate(shardParticles, this.transform.position, rotation) as GameObject;
				particle.rigidbody2D.AddForce(new Vector3(Random.Range (-359, 359), Random.Range (-359, 359), 0) * explosionForce);
			}
			PlayerPrefs.SetInt(Application.loadedLevelName + "_crystal", 1);
			Destroy(this.gameObject);
		}
	}
}
