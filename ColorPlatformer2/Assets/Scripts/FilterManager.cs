using UnityEngine;
using System.Collections;

public class FilterManager : MonoBehaviour {

	private const int COLOR1 = 0;
	private const int COLOR2 = 1;
	private const int COLOR3 = 2;

    const float DESAT_AMT = 0.5f;
    const float FADE_ALPHA = 0.35f;

	public AudioClip filterClip;

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
		oldGravity = 10;

        startColor = startColor;
		SetColorFilter(startColor);
		if(PlayerPrefs.GetInt("Filtering") == 1) {
			powerToFilter = true;
		} else if (PlayerPrefs.GetInt ("Filtering") == 0) {
			//powerToFilter = false;
		}

		filterClip = Resources.Load("filtering") as AudioClip;
	}

    void Awake()
    {
		_sprites = GetComponentsInChildren<SpriteRenderer>();
		_platforms = new BoxCollider2D[_sprites.Length];
        //_platforms = GetComponentsInChildren<BoxCollider2D>();
        //_sprites = new SpriteRenderer[_platforms.Length];
        for (int i = 0; i < _sprites.Length; i++)
        {
			BoxCollider2D col = _sprites[i].GetComponent<BoxCollider2D>();
			if(col == null) {
				_platforms[i] = null;
			} else {
				_platforms[i] = col;
				_sprites[i].sortingOrder = 1;
			}
            //_sprites[i] = _platforms[i].GetComponent<SpriteRenderer>();
            //_sprites[i].sortingOrder = 1;
        }
    }
	// Update is called once per frame
	void Update () {
		if(powerToFilter) {
			if(Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("Blue")) {
				GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<AudioSource>().PlayOneShot(filterClip);
	 
	            if (startColor == 0)
	            {
	                startColor = 2;
	            }
	            else
	            {
	                startColor -= 1;
	            }
				
				startColor = startColor % 3;
				SetColorFilter((startColor)); 
			} /*else if (Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Red") != 0) {
				SetColorFilter(COLOR2);
			} */else if (Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown("Yellow")) {
				GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<AudioSource>().PlayOneShot(filterClip);
	            startColor += 1;
				
				startColor = startColor % 3;
				SetColorFilter(startColor); 
	        }
		}
	}

	public void SetColorFilter(int color) {

		startColor = color;

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
			_player.gameObject.GetComponent<FreezeWeight>().UnFreeze();
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
			if(platform != null) {
				if (platform.tag == activeFilter) {
					render.color = Color.white;
	                //render.material = defaultShader;
	                render.sortingOrder = 0;
	                platform.enabled = true;
				} else {
					render.color = DesaturateColor(render.color);
	                //render.material = filterShader;
	                render.sortingOrder = 0;
					platform.enabled = false;
				}

				if(lastActiveFilter == "neon_blue_ground") {
					if(platform.tag == "neon_blue_ground") {
						BlueGate gate = platform.GetComponent<BlueGate>();
						if(gate != null) {
							gate.TriggerOff();
						}
					}
				}
			} else {
				if (render.gameObject.tag == activeFilter) {
					render.color = Color.white;
					//render.material = defaultShader;
					render.sortingOrder = 0;
				} else {
					render.color = DesaturateColor(render.color);
					//render.material = filterShader;
					render.sortingOrder = 0;
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

    public Color DesaturateColor(Color curr)
    {
        HSBColor hsb = new HSBColor(curr);

        HSBColor desaturated_hsb = new HSBColor(hsb.h,
                                                hsb.s * DESAT_AMT,
                                                hsb.b);
        desaturated_hsb.a = FADE_ALPHA;
        return desaturated_hsb.ToColor();

    }

    public struct HSBColor
    {
        public float h;
        public float s;
        public float b;
        public float a;

        public HSBColor(float h, float s, float b, float a)
        {
            this.h = h;
            this.s = s;
            this.b = b;
            this.a = a;
        }

        public HSBColor(float h, float s, float b)
        {
            this.h = h;
            this.s = s;
            this.b = b;
            this.a = 1f;
        }

        public HSBColor(Color col)
        {
            HSBColor temp = FromColor(col);
            h = temp.h;
            s = temp.s;
            b = temp.b;
            a = temp.a;
        }

        public static HSBColor FromColor(Color color)
        {
            HSBColor ret = new HSBColor(0f, 0f, 0f, color.a);

            float r = color.r;
            float g = color.g;
            float b = color.b;

            float max = Mathf.Max(r, Mathf.Max(g, b));

            if (max <= 0)
            {
                return ret;
            }

            float min = Mathf.Min(r, Mathf.Min(g, b));
            float dif = max - min;

            if (max > min)
            {
                if (g == max)
                {
                    ret.h = (b - r) / dif * 60f + 120f;
                }
                else if (b == max)
                {
                    ret.h = (r - g) / dif * 60f + 240f;
                }
                else if (b > g)
                {
                    ret.h = (g - b) / dif * 60f + 360f;
                }
                else
                {
                    ret.h = (g - b) / dif * 60f;
                }
                if (ret.h < 0)
                {
                    ret.h = ret.h + 360f;
                }
            }
            else
            {
                ret.h = 0;
            }

            ret.h *= 1f / 360f;
            ret.s = (dif / max) * 1f;
            ret.b = max;

            return ret;
        }

        public static Color ToColor(HSBColor hsbColor)
        {
            float r = hsbColor.b;
            float g = hsbColor.b;
            float b = hsbColor.b;
            if (hsbColor.s != 0)
            {
                float max = hsbColor.b;
                float dif = hsbColor.b * hsbColor.s;
                float min = hsbColor.b - dif;

                float h = hsbColor.h * 360f;

                if (h < 60f)
                {
                    r = max;
                    g = h * dif / 60f + min;
                    b = min;
                }
                else if (h < 120f)
                {
                    r = -(h - 120f) * dif / 60f + min;
                    g = max;
                    b = min;
                }
                else if (h < 180f)
                {
                    r = min;
                    g = max;
                    b = (h - 120f) * dif / 60f + min;
                }
                else if (h < 240f)
                {
                    r = min;
                    g = -(h - 240f) * dif / 60f + min;
                    b = max;
                }
                else if (h < 300f)
                {
                    r = (h - 240f) * dif / 60f + min;
                    g = min;
                    b = max;
                }
                else if (h <= 360f)
                {
                    r = max;
                    g = min;
                    b = -(h - 360f) * dif / 60 + min;
                }
                else
                {
                    r = 0;
                    g = 0;
                    b = 0;
                }
            }

            return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), hsbColor.a);
        }

        public Color ToColor()
        {
            return ToColor(this);
        }

        public override string ToString()
        {
            return "H:" + h + " S:" + s + " B:" + b;
        }

        public static HSBColor Lerp(HSBColor a, HSBColor b, float t)
        {
            float h, s;

            //check special case black (color.b==0): interpolate neither hue nor saturation!
            //check special case grey (color.s==0): don't interpolate hue!
            if (a.b == 0)
            {
                h = b.h;
                s = b.s;
            }
            else if (b.b == 0)
            {
                h = a.h;
                s = a.s;
            }
            else
            {
                if (a.s == 0)
                {
                    h = b.h;
                }
                else if (b.s == 0)
                {
                    h = a.h;
                }
                else
                {
                    // works around bug with LerpAngle
                    float angle = Mathf.LerpAngle(a.h * 360f, b.h * 360f, t);
                    while (angle < 0f)
                        angle += 360f;
                    while (angle > 360f)
                        angle -= 360f;
                    h = angle / 360f;
                }
                s = Mathf.Lerp(a.s, b.s, t);
            }
            return new HSBColor(h, s, Mathf.Lerp(a.b, b.b, t), Mathf.Lerp(a.a, b.a, t));
        }
    }
}
