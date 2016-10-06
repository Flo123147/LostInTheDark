using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour, DoesNotBlock {

	public static Color[] colors = new Color[6]{Color.yellow,Color.green,Color.blue,Color.red,Color.white,Color.black};

	public int color;
	public SpriteRenderer sr;

	public bool reset;
	public int resetList;


	private Player pickedUpBy;

	private bool pickUpAble;
	// Use this for initialization
	void Awake () {
		sr.color = Key.colors [color];
		pickUpAble = true;
	}
	public bool isBlocking(){
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		if (reset) {
			reset = false;
		}
	}

	public void PickedUp(Player player){
		sr.enabled = false;
		pickedUpBy = player;
		pickUpAble = false;
		//ToDO: Sound
	}

	public void OnTriggerStay2D(Collider2D other){
		if (pickUpAble) {
			Player player = other.gameObject.GetComponent<Player> ();
			if (player) {
				player.KeyPickup (this, color);
			} else {
			}
		}
	}
}
