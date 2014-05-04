using UnityEngine;
using System.Collections;

public class FadeInStart : MonoBehaviour {

	public float alphaChangeRate = 0.75f;

	private bool changeAlpha = true;
	private SpriteRenderer renderer;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(changeAlpha) {
			FadeIn();
		}
	}

	void FadeIn() {
		if(this.gameObject.GetComponent<SpriteRenderer>().color.a >= 1) {
			Color newAlpha = this.gameObject.GetComponent<SpriteRenderer>().color;
			newAlpha.a = 1;
			this.gameObject.GetComponent<SpriteRenderer>().color = newAlpha;
			changeAlpha = false;
		} else {
			Color newAlpha = this.gameObject.GetComponent<SpriteRenderer>().color;
			newAlpha.a += (alphaChangeRate * Time.deltaTime);
			this.gameObject.GetComponent<SpriteRenderer>().color = newAlpha;
		}
	}
}
