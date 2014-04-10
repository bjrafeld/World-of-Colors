using UnityEngine;
using System.Collections;

public class CharacterPhysics: MonoBehaviour 
{	
	public enum inputState 
	{ 
		None, 
		WalkLeft, 
		WalkRight, 
		Jump, 
		Pass
	}
	[HideInInspector] public inputState currentInputState;
	
	[HideInInspector] public enum facing { Right, Left }
	[HideInInspector] public facing facingDir;
	
	[HideInInspector] public bool alive = true;
	[HideInInspector] public Vector3 spawnPos;
	
	protected Transform _transform;
	protected Rigidbody2D _rigidbody;
	
	// edit these to tune character movement	
	public float runVel = 2.5f; 	// run speed
	public float jumpVel = 4f; 	// jump velocity
	
	private float moveVel;
	private float pVel = 0f;

	private Vector2 physVel = new Vector2();
	[HideInInspector] public bool grounded = false;
	
	public virtual void Awake()
	{
		_transform = transform;
		_rigidbody = rigidbody2D;
	}
	
	// Use this for initialization
	public virtual void Start () 
	{
		moveVel = runVel;
	}
	
	// Update is called once per frame
	public virtual void UpdateMovement() 
	{	
		// teleport me to the other side of the screen when I reach the edge
		if(_transform.position.x > 4f)
		{
			_transform.position = new Vector3(-4f,_transform.position.y, 0);
		}
		if(_transform.position.x < -4f)
		{
			_transform.position = new Vector3(4f,_transform.position.y, 0);
		}
	}
	
	// ============================== FIXEDUPDATE ============================== 

	public void FixedUpdate()
	{
		// inputstate is none unless one of the movement keys are pressed
		currentInputState = inputState.None;

		physVel = Vector2.zero;
		
		// move left
		if(Input.GetKey(KeyCode.LeftArrow)) 
		{ 
			currentInputState = inputState.WalkLeft;
			physVel.x = -moveVel;
			facingDir = facing.Left;
		}
		
		// move right
		if (Input.GetKey(KeyCode.RightArrow) && currentInputState != inputState.WalkLeft) 
		{ 
			currentInputState = inputState.WalkRight;
			physVel.x = moveVel;
			facingDir = facing.Right;
		}
		
		// jump
		if (Input.GetKey(KeyCode.Space)) 
		{ 
			currentInputState = inputState.Jump;
			if(grounded)
			{
				_rigidbody.velocity = new Vector2(physVel.x, jumpVel);
				grounded = false;
			}
		}

		_rigidbody.velocity = new Vector2(physVel.x, _rigidbody.velocity.y);

	}

	public void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.tag == "Ground") {
			Debug.Log ("Hit the ground.");
			grounded = true;
		}
	}
}

public enum MyTeam 
{
	Team1,
	Team2,
	None
}
