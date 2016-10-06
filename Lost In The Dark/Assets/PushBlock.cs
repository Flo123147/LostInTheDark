using UnityEngine;
using System.Collections;

public class PushBlock : MonoBehaviour {

	public int resetList;
	private Vector3 startPos;
	public BoxCollider2D bc,t_bc;

	private Triggerable lastTrigged;
	// Use this for initialization
	void Awake () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void Translate(Vector3 vec){
		
		bc.enabled = t_bc.enabled = false;
		bool hitWall = false;
		bool hitNewTrigger = false;
		Triggerable toTrigger = null;
		RaycastHit2D[] checkGeomty = Physics2D.RaycastAll (transform.position, vec, 1);
		foreach (RaycastHit2D rch in checkGeomty) {
			Triggerable triggerable = rch.transform.GetComponent<Triggerable> ();
			DoesNotBlock doesNotBlock = rch.transform.GetComponent<DoesNotBlock> ();

			if (triggerable != null && doesNotBlock == null) {
				hitNewTrigger = true;
			}
			if (triggerable != null && doesNotBlock != null && !doesNotBlock.isBlocking()) {
				hitNewTrigger = true;
			}

			if (triggerable != null && doesNotBlock != null && doesNotBlock.isBlocking()) {
				hitWall = true;
			}

			if (triggerable == null && doesNotBlock == null) {
				hitWall = true;
			}

			if (triggerable == null && doesNotBlock != null && doesNotBlock.isBlocking()) {
				hitWall = true;
			}

			if (hitNewTrigger) {
				if (lastTrigged == null || lastTrigged != triggerable) {
					toTrigger = triggerable;
				} else {
					hitNewTrigger = false;
				}
			}
		}
		if (!hitWall) {
			transform.Translate (vec);
			if (lastTrigged != null) {
				lastTrigged.SwitchOff ();
				lastTrigged = null;
			}
			if (hitNewTrigger) {
				toTrigger.Trigger (this.transform);
				lastTrigged = toTrigger;
			}
		}


		bc.enabled = t_bc.enabled = true;
	}
		


		
	public void OnTriggerEnter2D(Collider2D other){
		Triggerable triggerable = other.GetComponent<Triggerable> ();
		if (triggerable!= null) {
			triggerable.Trigger (this.transform);
		}

		Player player = other.gameObject.GetComponent<Player> ();
		if (player) {
			Vector3 vec = transform.position - player.transform.position;
			if (Mathf.Abs (vec.x) > Mathf.Abs (vec.y)) {
				Translate (new Vector3 (Mathf.Round (vec.x), 0, 0));
			} else {
				Translate (new Vector3(0,Mathf.Round(vec.y),0));
			}
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
}