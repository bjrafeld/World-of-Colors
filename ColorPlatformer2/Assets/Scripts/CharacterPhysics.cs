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

	private Vector3 center;
	private Vector3 size;

	
	public virtual void Awake()
	{
		_transform = transform;
		_rigidbody = rigidbody2D;
        groundCheck = transform.Find("groundCheck");
	}
	
	// Use this for initialization
	public virtual void Start () 
	{
		center = GetComponent<BoxCollider2D>().center;
		size = GetComponent<BoxCollider2D>().size;
	}

	
	// ============================== FIXEDUPDATE ============================== 

	public void FixedUpdate()
	{
		// inputstate is none unless one of the movement keys are pressed
		currentInputState = inputState.None;


        if (Physics2D.Linecast(_transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            grounded = true;
        }
        
        Debug.Log(grounded);

		_rigidbody.velocity = new Vector2(physVel.x, _rigidbody.velocity.y);

	}

    public void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

		if (Input.GetKey(KeyCode.Escape)) {
			Application.LoadLevel("main_menu");
		}

        physVel = Vector2.zero;
        //Handles joystick movement.
        float hor = Input.GetAxis("HorizontalJoy");
        bool leftStick = hor < 0;
        bool rightStick = hor > 0;
        
        // move left
        if (Input.GetKey(KeyCode.LeftArrow) || leftStick)
        {
            currentInputState = inputState.WalkLeft;
            facingDir = facing.Left;
            if (leftStick)
            {
                physVel.x = runVel * hor;
            }
            else
            {
                physVel.x = -runVel;
            }
        }

        // move right
        if ((Input.GetKey(KeyCode.RightArrow) || rightStick) && currentInputState != inputState.WalkLeft)
        {
            currentInputState = inputState.WalkRight;
            facingDir = facing.Right;
            if (rightStick)
            {
                physVel.x = runVel * hor;
            }
            else
            {
                physVel.x = runVel;
            }
        }

        // jump
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Jump") != 0 || Input.GetAxis("JumpAxis") > 0)
        {
            currentInputState = inputState.Jump;
            if (grounded)
            {
                _rigidbody.velocity = new Vector2(physVel.x, jumpVel);
                grounded = false;
            }
        }

        if (currentInputState != inputState.Jump && hor == 0)
        {
            currentInputState = inputState.None;
        }
    }
	public void OnCollisionEnter2D(Collision2D col) {
		/*if (col.gameObject.layer == groundMask) {
			Debug.Log ("Hit the ground.");
			grounded = true;
		}*/
	}
}
