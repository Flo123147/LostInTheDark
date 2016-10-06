using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour, Triggerable{


	public Teleporter teleportTo;

	private bool ready;
	// Use this for initialization
	void Awake () {
		ready = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void Transport(){
		ready = false;
	}

	public void Trigger (Transform tr){
		if (ready) {
			Player player = tr.GetComponent<Player> ();
			if (player) {
				teleportTo.Transport ();
				Vector3 vec = teleportTo.transform.position;
				player.transform.position = new Vector3 (vec.x, vec.y, player.transform.position.z);
			}
		}
	}
	public void StayOn (){
		
	}

	public void SwitchOff(){
		ready = true;
	}
}
