using UnityEngine;
using System.Collections;

public class FilterManager : MonoBehaviour {

	private const int COLOR1 = 0;
	private const int COLOR2 = 1;
	private const int COLOR3 = 2;

	public int startColor;

    public Material filterShader;
    public Material defaultShader;

    public string colorTag1;
	public string colorTag2;
	public string colorTag3;

	private string activeFilter;
	private string lastActiveFilter;
    private BoxCollider2D[] _platforms;
    private SpriteRenderer[] _sprites;
	//For no assets
	private Color activeColor;

	public float transparency = 0.3f;

	//Used for spawning on filtering
	public TriggerSpawner spawner;
	public string spawnColor;
	public bool weightSpawnColor = false;

	public TriggerSpawner spawner2;
	public string spawnColor2;
	public bool weightSpawnColor2 = false;

	public TriggerSpawner spawner3;
	public string spawnColor3;
	public bool weightSpawnColor3 = false;

	private CharacterPhysics _player;
	private Rigidbody2D _playerRigidBody;
	private float oldGravity;


	//MAKE SURE TO CHANGE
	public static bool powerToFilter = true;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterPhysics>() as CharacterPhysics;
		_playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>() as Rigidbody2D;
		oldGravity = _playerRigidBody.gravityScale;

        startColor = startColor;
		SetColorFilter(startColor);
	}

    void Awake()
    {
        _platforms = GetComponentsInChildren<BoxCollider2D>();
        _sprites = new SpriteRenderer[_platforms.Length];
        for (int i = 0; i < _platforms.Length; i++)
        {
            _sprites[i] = _platforms[i].GetComponent<SpriteRenderer>();
            _sprites[i].sortingOrder = 1;
        }
    }
	// Update is called once per frame
	void Update () {
		if(powerToFilter) {
			if(Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("Blue")) {
	 
	            if (startColor == 0)
	            {
	                startColor = 2;
	            }
	            else
	            {
	                startColor -= 1;
	            }
	            SetColorFilter((startColor % 3)); 
			} /*else if (Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Red") != 0) {
				SetColorFilter(COLOR2);
			} */else if (Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown("Yellow")) {
	            startColor += 1;
	            SetColorFilter((startColor % 3));
	        }
		}
	}

	public void SetColorFilter(int color) {

		lastActiveFilter = activeFilter;

		if(color == COLOR1) {
			activeFilter = colorTag1;
		} else if (color == COLOR2) {
			activeFilter = colorTag2;
		} else if (color == COLOR3) {
			activeFilter = colorTag3;
		}

		if(weightSpawnColor) {
			if(activeFilter == spawnColor) {
				spawner.spawnWeight();
			}
		}

		if(weightSpawnColor2) {
			if(activeFilter == spawnColor2) {
				spawner2.spawnWeight();
			}
		}

		if(weightSpawnColor3) {
			if(activeFilter == spawnColor3) {
				spawner3.spawnWeight();
			}
		}

		if(lastActiveFilter == "neon_red_ground") {
			_player.movementFrozen = false;
			_playerRigidBody.gravityScale = oldGravity;
			GameObject[] weights = GameObject.FindGameObjectsWithTag("weight") as GameObject[];
			foreach (GameObject w in weights) {
				w.GetComponent<FreezeWeight>().UnFreeze();
			}
		}

		for(int i = 0; i < _platforms.Length; i++) {
            BoxCollider2D platform = _platforms[i];
            SpriteRenderer render = _sprites[i];
			if (platform.tag == activeFilter) {
				//render.color = Color.white;
                render.material = defaultShader;
                render.sortingOrder = 0;
                platform.enabled = true;
			} else {
				//Color transparent = Color.white;
				//transparent.a = transparency;
				//render.color = transparent;
                render.material = filterShader;
                render.sortingOrder = 0;
				platform.enabled = false;
			}

			if(lastActiveFilter == "neon_blue_ground") {
				if(platform.tag == "neon_blue_ground") {
					platform.GetComponent<BlueGate>().TriggerOff();
				}
			}
		}
	}

	public void SetColorFilterTag(string tagName) {
		if(tagName == colorTag1) {
			SetColorFilter(0);
		} else if (tagName == colorTag2) {
			SetColorFilter(1);
		} else if (tagName == colorTag3) {
			SetColorFilter(2);
		}
	}
}
