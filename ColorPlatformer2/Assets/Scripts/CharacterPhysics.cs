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

	public AudioClip jump1;
	public AudioClip jump2;
	public AudioClip jump3;

	protected Transform _transform;
	protected Rigidbody2D _rigidbody;
    protected Animator _anim;
	
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

	public bool movementFrozen = false;

	
	public virtual void Awake()
	{
		_transform = transform;
		_rigidbody = rigidbody2D;
        _anim = GetComponent<Animator>();
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


	private void togglePause() {
		PauseMenu menu = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PauseMenu>();
		if(paused) {
			GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PauseMenu>().UnPause();
		} else {
			GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PauseMenu>().Pause ();
		}
	}

	private void pause(bool freeze) {
		PauseMenu menu = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PauseMenu>();
		paused = freeze;
		if(paused) {
			GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PauseMenu>().Pause ();
		} else {
			
			Time.timeScale = 1;
			GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<PauseMenu>().UnPause ();
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
			if(paused) {
				pause (false);
			} else {
				pause (true);
			}
            Debug.Log(Input.GetButtonDown("Pause"));
        }

        physVel = Vector2.zero;
        //Handles joystick movement.
        float hor = Input.GetAxis("HorizontalJoy");
        bool leftStick = hor < 0;
        bool rightStick = hor > 0;

        
        // move left
        if (Input.GetKey(KeyCode.LeftArrow) || leftStick && currentInputState != inputState.WalkRight)
        {
            currentInputState = inputState.WalkLeft;
            facingDir = facing.Left;
	        if(!movementFrozen) {    
				if (leftStick)
	            {
	                physVel.x = runVel * hor;
	            }
	            else
	            {
	                physVel.x = -runVel;
	            }
			}
        }

        // move right
        if ((Input.GetKey(KeyCode.RightArrow) || rightStick) && currentInputState != inputState.WalkLeft)
        {
            currentInputState = inputState.WalkRight;
            facingDir = facing.Right;
			if(!movementFrozen) {
	            if (rightStick)
	            {
	                physVel.x = runVel * hor;
	            }
	            else
	            {
	                physVel.x = runVel;
	            }
			}
        }

        // jump
		if(!movementFrozen) {
	        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Jump") != 0) // || Input.GetAxis("JumpAxis") > 0)
	        {
	            currentInputState = inputState.Jump;
	            if (grounded)
	            {
	                _rigidbody.velocity = new Vector2(physVel.x, jumpVel);
	                grounded = false;
					int jumpIndex = Random.Range (0, 3);
					if(jumpIndex == 0) {
						audio.PlayOneShot(jump1);
					} else if (jumpIndex == 1) {
						audio.PlayOneShot(jump2);
					} else if (jumpIndex == 2) {
						audio.PlayOneShot(jump3);
					}
	            }
	        }
		}

        if (currentInputState != inputState.Jump)
        {
            if (physVel.x == 0)
            {
                currentInputState = inputState.None;
                _anim.SetInteger("state", 0);
            } 
            else if (physVel.x > 0) 
            {
                _anim.SetInteger("state", 1);
            }
            else if (physVel.x < 0)
            {
                _anim.SetInteger("state", 4);
            }
        }

    }
	public void OnCollisionEnter2D(Collision2D col) {
		/*if (col.gameObject.layer == groundMask) {
			Debug.Log ("Hit the ground.");
			grounded = true;
		}*/
	}
    public void DeathAnim()
    {
        _anim.SetInteger("state", 3);
        this.gameObject.collider2D.enabled = false;
        this.rigidbody2D.gravityScale = 0;
    }
	public void KillPlayer() {
		Application.LoadLevel(Application.loadedLevelName);
	}
}
