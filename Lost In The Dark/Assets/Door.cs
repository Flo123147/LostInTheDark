using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, DoesNotBlock {

	public SpriteRenderer sr,sr_StateOpen;
	public BoxCollider2D bc;

	private bool isClosed;
	public bool default_isClosed;
	public int color;

	public bool open,close,reset;

	private Color doorColor;

	// Use this for initialization


	void Awake () {
		sr.color = Key.colors [color];
		sr_StateOpen.color = Key.colors [color];
		doorColor = Key.colors [color];
		isClosed = default_isClosed;
	}

		
	// Update is called once per frame
	void Update () {
		if (open) {
			open = false;
			Open ();
		}
		if (close) {
			close = false;
			Close ();
		}
		if (reset) {
			reset = false;
		}
	}

	public bool isBlocking(){
		if (isClosed) {
			return true;
		} else {
			return false;
		}
	}

	public void Open(){
		
		if (isClosed) {
			isClosed = false;
			bc.enabled = false;
			StopCoroutine ("Fade");
			StartCoroutine ("Fade", true);			
		}
	}

	public void Close(){	
		if (!isClosed) {
			isClosed = true;
			bc.enabled = true;
			StopCoroutine ("Fade"); 
			StartCoroutine ("Fade", false);
		}

	}

	private IEnumerator Fade(bool opening){
		if (opening) {
			for (float i = 1; i >= -0.1f; i -= 0.1f) {
				sr.color = new Color (doorColor.r, doorColor.g, doorColor.b, i);
				yield return new WaitForSeconds (0.05f);
			}

		} else {
			for (float i = 0; i < 1.1f; i += 0.1f) {
				sr.color = new Color (doorColor.r, doorColor.g, doorColor.b, i);
				yield return new WaitForSeconds (0.05f);
			}		
			sr.color = Key.colors [color];
		}
	}


	public void OnCollisionEnter2D(Collision2D collision){
		Player player = collision.gameObject.GetComponent<Player> ();
		if (player) {
			if (isClosed && player.UseKey (color)) {
				Open ();
			}
		} else {
			Debug.Log ("LEL");

		}
	}

	public void OnTriggerEnter2D(Collider2D other){


	}

	public void OnTriggerStay2D(Collider2D other){
	}

	public void OnTriggerExit2D(Collider2D other){
	}
}
