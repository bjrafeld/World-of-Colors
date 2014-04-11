using UnityEngine;
using System.Collections;

public class FilterManager : MonoBehaviour {

	private const int COLOR1 = 1;
	private const int COLOR2 = 2;
	private const int COLOR3 = 3;

	public string colorTag1;
	public string colorTag2;
	public string colorTag3;

	private string activeFilter;

	//For no assets
	private Color activeColor;

	public Color color1;
	public Color color2;
	public Color color3;
	public Color offColor;

	// Use this for initialization
	void Start () {
		SetColorFilter(COLOR1);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)) {
			SetColorFilter(COLOR1);
		} else if (Input.GetKeyDown(KeyCode.S)) {
			SetColorFilter(COLOR2);
		} else if (Input.GetKeyDown(KeyCode.D)) {
			SetColorFilter(COLOR3);
		}
	}

	void SetColorFilter(int color) {

		if(color == COLOR1) {
			activeFilter = colorTag1;
			activeColor = color1;
		} else if (color == COLOR2) {
			activeFilter = colorTag2;
			activeColor = color2;
		} else if (color == COLOR3) {
			activeFilter = colorTag3;
			activeColor = color3;
		}
		BoxCollider2D[] platforms = GetComponentsInChildren<BoxCollider2D>();

		foreach(BoxCollider2D platform in platforms) {
			if (platform.tag == activeFilter) {
				platform.gameObject.renderer.material.color = activeColor;
				platform.enabled = true;
			} else {
				platform.gameObject.renderer.material.color = offColor;
				platform.enabled = false;
			}
		}
	}
}
