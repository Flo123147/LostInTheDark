using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour, Triggerable{


	public Teleporter teleportTo;
	public float teleportTime = 0.7f;

	private bool ready;
	// Use this for initialization
	void Awake () {
		ready = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReadyTransport(){
		ready = false;
	}

	private IEnumerator Transport(Player player){
		float currLerp = 0;
		float targetLerp = 1;
		float lerpTime = teleportTime;

		float camStart = Camera.main.orthographicSize;
		float camEnd = camStart / 2;

		float playerViewRange = player.maxLightLengt;
		player.maxLightLengt = 0;

		player.Teleport (true);

		Vector3 startPos = new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z), endPos = new Vector3(transform.position.x,transform.position.y,player.transform.position.z);

		while (currLerp < targetLerp) {

			currLerp += Time.deltaTime/lerpTime;
			if (currLerp > targetLerp) {
				currLerp = targetLerp;
			}

			Camera.main.orthographicSize = Mathf.Lerp (camStart, camEnd, currLerp);

			player.transform.position = Vector3.Lerp (startPos,endPos,currLerp);

			yield return new WaitForEndOfFrame ();

		}

		player.transform.position = new Vector3(teleportTo.transform.position.x,teleportTo.transform.position.y,player.transform.position.z);

		player.maxLightLengt = playerViewRange;
		player.Teleport (false);

		currLerp = 0;

		while (currLerp < targetLerp) {

			currLerp += Time.deltaTime/lerpTime;
			if (currLerp > targetLerp) {
				currLerp = targetLerp;
			}

			Camera.main.orthographicSize = Mathf.Lerp (camEnd, camStart, currLerp);

			yield return new WaitForEndOfFrame ();

		}
		Camera.main.orthographicSize = camStart;

	}

	public void Trigger (Transform tr){
		if (ready) {
			Player player = tr.GetComponent<Player> ();
			if (player) {
				teleportTo.ReadyTransport ();
				StopCoroutine("Transport");
				StartCoroutine ("Transport",player);
			}
		}
	}
	public void StayOn (){
		
	}

	public void SwitchOff(){
		ready = true;
	}
}
