﻿using UnityEngine;
using System.Collections;

public class FilterManager : MonoBehaviour {

	private const int COLOR1 = 1;
	private const int COLOR2 = 2;
	private const int COLOR3 = 3;

	public int startColor = 1;

	public string colorTag1;
	public string colorTag2;
	public string colorTag3;

	private string activeFilter;
    private BoxCollider2D[] _platforms;

	//For no assets
	private Color activeColor;

	public float transparency = 0.3f;
	

	// Use this for initialization
	void Start () {
		SetColorFilter(startColor);
	}

    void Awake()
    {
        _platforms = GetComponentsInChildren<BoxCollider2D>();
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
		} else if (color == COLOR2) {
			activeFilter = colorTag2;
		} else if (color == COLOR3) {
			activeFilter = colorTag3;
		}

		foreach(BoxCollider2D platform in _platforms) {
			SpriteRenderer render;
			if (platform.tag == activeFilter) {
				render = platform.gameObject.GetComponent<SpriteRenderer>();
				render.color = Color.white;
				platform.enabled = true;
			} else {
				render = platform.gameObject.GetComponent<SpriteRenderer>();
				Color transparent = Color.white;
				transparent.a = transparency;
				render.color = transparent;
				platform.enabled = false;
			}
		}
	}
}
