using UnityEngine;
using System.Collections;

public class ConstantDown : MonoBehaviour {

	public float speed = 5f;
	public Vector3 direction;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			this.transform.position = (this.transform.position) + (direction * (speed*Time.deltaTime));
		
	}

}
