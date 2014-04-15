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

	private Vector2 physVel = new Vector2();
    private Transform groundCheck;
	[HideInInspector] public bool grounded = false;
	
	public virtual void Awake()
	{
		_transform = transform;
		_rigidbody = rigidbody2D;
        groundCheck = transform.Find("groundCheck");
	}
	
	// Use this for initialization
	public virtual void Start () 
	{

	}

	
	// ============================== FIXEDUPDATE ============================== 

	public void FixedUpdate()
	{
		// inputstate is none unless one of the movement keys are pressed
		currentInputState = inputState.None;

		physVel = Vector2.zero;

        grounded = Physics2D.Linecast(_transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.Log(grounded);
		// move left
		if(Input.GetKey(KeyCode.LeftArrow)) 
		{ 
			currentInputState = inputState.WalkLeft;
			physVel.x = -runVel;
			facingDir = facing.Left;
		}
		
		// move right
		if (Input.GetKey(KeyCode.RightArrow) && currentInputState != inputState.WalkLeft) 
		{ 
			currentInputState = inputState.WalkRight;
			physVel.x = runVel;
			facingDir = facing.Right;
		}
		
		// jump
		if (Input.GetKey(KeyCode.Space) || Input.GetKey (KeyCode.UpArrow)) 
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

    public void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
	public void OnCollisionEnter2D(Collision2D col) {
		/*if (col.gameObject.layer == groundMask) {
			Debug.Log ("Hit the ground.");
			grounded = true;
		}*/
	}
}
