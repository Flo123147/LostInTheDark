using UnityEngine;
using System.Collections;

public class DoorKeyless : MonoBehaviour , Triggerable, LogicReciever, DoesNotBlock{

	public SpriteRenderer sr,sr_BackG;
	public BoxCollider2D bc;

	public bool defaultIsClosed;
	private bool isClosed;

	public bool isBlocked;

	public int logicGroup;
	private bool logicOn;
	public Color color;

	private GameManager gm;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		bc = GetComponent<BoxCollider2D> ();
		isClosed = defaultIsClosed;
		gm = GameManager.gm;
		color = gm.GetColorForLGroup (logicGroup);
		sr.color = color;
		sr_BackG.color = color;
		if (!isClosed) {
			isClosed = true;
			Open ();
		}
	
		Register ();
	}
	
	// Update is called once per frame
	void Update () {
		if (defaultIsClosed) {	
			if (logicOn) {
				TurnOn ();		
			} else {
				TurnOff ();
			}
		} else {
			if (logicOn) {
				TurnOff ();		
			} else {
				TurnOn ();
			}
		}
	}

	public bool isBlocking(){
		if (isClosed) {
			return true;
		} else {
			return false;
		}
	}

	private void TurnOn(){
		Open ();
	}

	private void TurnOff (){
		Close ();
	}

	public void setLogicStatus (bool on){
		
		logicOn = on;
	}
	public void Register (){
		gm.RegisterLogicReciever (this);
	}
	public int GetGroup (){
		return logicGroup;
	}

	public void Trigger (Transform tr){
		isBlocked = true;
	}
	public void StayOn (){
		isBlocked = true;
	}
	public void SwitchOff(){
		isBlocked = false;
	}

	public void Open(){
		
		if (isClosed) {
			isClosed = false;
			bc.enabled = false;
			StopCoroutine ("Fade");
			StartCoroutine ("Fade", true);	
		}
	}

	private IEnumerator WaitForUnblock(){
		while (isBlocked) {
			yield return new WaitForEndOfFrame ();
		}
		Close ();
	}

	public void Close(){	
		if (isBlocked) {
			StartCoroutine ("WaitForUnblock");
		} else {
			if (!isClosed) {
				isClosed = true;
				bc.enabled = true;
				StopCoroutine ("Fade"); 
				StartCoroutine ("Fade", false);
			}
		}
	}

	private IEnumerator Fade(bool opening){
		if (opening) {
			for (float i = 1; i >= -0.1f; i -= 0.1f) {
				sr.color = new Color (color.r, color.g, color.b, i);
				yield return new WaitForSeconds (0.02f);
			}

		} else {
			for (float i = 0; i < 1.1f; i += 0.1f) {
				sr.color = new Color (color.r, color.g, color.b, i);
				yield return new WaitForSeconds (0.02f);
			}		
		}
	}

}

