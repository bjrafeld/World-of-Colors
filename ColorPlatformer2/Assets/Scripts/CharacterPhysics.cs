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

    private int startMenu;

	protected Transform _transform;
	protected Rigidbody2D _rigidbody;
	
	// edit these to tune character movement	
	public float runVel = 2.5f; 	// run speed
	public float jumpVel = 4f; 	// jump velocity

	public float buttonHeight = 30f;
	public float buttonWidth = 100f;
	
	private float moveVel;

	private Vector2 physVel = new Vector2();
    private Transform groundCheck, groundCheck1, groundCheck2;
	[HideInInspector] public bool grounded = false;

	private Vector3 center;
	private Vector3 size;

	private bool paused = false;

	
	public virtual void Awake()
	{
		_transform = transform;
		_rigidbody = rigidbody2D;
        groundCheck = transform.Find("groundCheck");
        groundCheck1 = transform.Find("groundCheck1");
        groundCheck2 = transform.Find("groundCheck2");
		pause (false);
        startMenu = 0;
	}
	
	// Use this for initialization
	public virtual void Start () 
	{
		center = GetComponent<BoxCollider2D>().center;
		size = GetComponent<BoxCollider2D>().size;

	}

	public void OnGUI() {
		if(paused) {
			displayPauseMenu();
		}
	}

	private void displayPauseMenu() {
		float centerPointX = (Camera.main.pixelWidth/2 - buttonWidth);
		float centerPointY = (0 + buttonHeight);
		if(GUI.Button (new Rect((centerPointX - (1/2)*buttonWidth), centerPointY , buttonWidth, buttonHeight), "Main Menu")) {
			Debug.Log ("Sandbox clicked");
			Application.LoadLevel("Main_menu");
		}
	}

	private void togglePause() {
		if(paused) {
			Time.timeScale = 1;
			paused = false;
		} else {
			Time.timeScale = 0;
			paused = true;
		}
	}

	private void pause(bool freeze) {
		paused = freeze;
		if(paused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
	// ============================== FIXEDUPDATE ============================== 

	public void FixedUpdate()
	{
		// inputstate is none unless one of the movement keys are pressed
		currentInputState = inputState.None;


        if (Physics2D.Linecast(_transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(_transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(_transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

		_rigidbody.velocity = new Vector2(physVel.x, _rigidbody.velocity.y);

	}

    public void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

		if (Input.GetButtonDown("Pause")) {
            startMenu++;
			togglePause();
            Debug.Log(Input.GetButtonDown("Pause"));
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
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Jump") != 0) // || Input.GetAxis("JumpAxis") > 0)
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
