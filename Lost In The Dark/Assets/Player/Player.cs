using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;
	public	Rigidbody2D rb;
	public BoxCollider2D bc;
	public float maxLightLengt;
	public Tile[] tiles;
	public End end;

	public float playerSpeed;
	float speedPerc;
		
	public Animator anim;

	private float angle;
	public int[] keys;

	public  float lerpTime;
	private float currLerpTime = 0f;

	public float speedResetAt;
	private float standstillTime = 0.2f;

	private bool teleportLock;

	public float camDist;
	// Use this for initialization
	void Awake () {

		GameObject[] tiles_go = GameObject.FindGameObjectsWithTag ("Tile");
		maxLightLengt = maxLightLengt * maxLightLengt;
		tiles = new Tile[tiles_go.Length];
		for (int i = 0; i < tiles_go.Length; i++) {
			tiles [i] = tiles_go [i].GetComponent<Tile> ();
		}
		//Debug.Log (tiles_go.Length);

		end = GameObject.FindGameObjectWithTag ("End").GetComponent<End>();

		keys = new int[6];

		angle = 0;

		if(lerpTime <= 0){
			Debug.LogError("LerpTime = 0!");
			lerpTime = 0.1f;
		}


		Camera.main.backgroundColor = Color.black;
		Camera.main.orthographicSize = 7;
	}

	void Start(){
		GameManager.gm.tiles = this.tiles;
	}

	void FixedUpdate() {
		Visible ();
	}
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.R)) {
			end.Reload ();
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			end.LoadLevelSelect ();
		}
		Vector2 vec = Vector2.zero;

		rb.velocity = Vector3.zero;

		float prevAngle = angle;


		if (!teleportLock) {

			float up = Input.GetAxisRaw ("Vertical");
			float right = Input.GetAxisRaw ("Horizontal");
			vec = new Vector2 (right, up);
			if (vec.sqrMagnitude > 1) {
				vec = vec.normalized;
			}

			if (up != 0 || right != 0) {
				angle = Vector2.Angle (Vector2.up, vec);
				if (right > 0) {
					angle = 180 + (180 - angle);
				}
			}


			angle = Mathf.LerpAngle (prevAngle, angle, 0.2f);
		
		}

		rb.MovePosition (transform.position + new Vector3 (vec.x, vec.y, 0) * Time.deltaTime *speed);

		transform.rotation = Quaternion.Euler (0, 0, angle);

		Camera.main.transform.position = transform.position + Vector3.back*camDist;

		if ((transform.position - end.transform.position).sqrMagnitude < 0.25f) {
			End ();
		}

		anim.SetFloat ("PlayerSpeed", vec.sqrMagnitude);
	}





	public void OnTriggerEnter2D(Collider2D other){
		Triggerable triggerable = other.GetComponent<Triggerable> ();
		if (triggerable!= null) {
			triggerable.Trigger (this.transform);
		}
	}

	public void OnTriggerStay2D(Collider2D other){
		Triggerable triggerable = other.GetComponent<Triggerable> ();
		if (triggerable != null) {
			triggerable.StayOn ();
		}
	}

	public void OnTriggerExit2D(Collider2D other){			
		Triggerable triggerable = other.GetComponent<Triggerable> ();
		if (triggerable!= null) {
			triggerable.SwitchOff ();
		}
	}

	public bool UseKey(int color){
		if (keys[color] > 0) {
			keys[color] -= 1;
			return true;
			
		} else {
			return false;
		}
				
	}

	public void KeyPickup(Key key,int color){	
		keys[color]++;
		key.PickedUp (this);
	}

	public void End(){
		end.EndLevel ();
		Debug.Log ("Won");
	}

	public void Teleport (bool start){
		if (start) {
			teleportLock = true;
		} else {
			teleportLock = false;
		}
	}

	public void Visible(){
		for (int i = 0; i < tiles.Length; i++) {
			float dist = (tiles [i].transform.position - this.transform.position).sqrMagnitude;
			if (dist > maxLightLengt) {
				tiles [i].Vis (false, 0, 0);
			} else {
				tiles [i].Vis (true,dist,maxLightLengt);
			}
		}
	}
}
