using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour, Triggerable, LogicSetter, DoesNotBlock {

	public SpriteRenderer sr;
	public bool on;
	public int logicGroup;
	public Color color;

	private GameManager gm;


	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		gm = GameManager.gm;
		color = gm.GetColorForLGroup (logicGroup);
		sr.color = color;
		Register ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool isBlocking(){
		return false;	
	}

	public bool GetStatus (){
		return this.on;
	}
	public int GetGroup(){
		return logicGroup;
	}
	public void Register (){
		gm.RegisterLogicSetter (this);
	}


	public void Trigger (Transform tr){
		ButtonOn ();
	}
	public void StayOn (){		
		ButtonOn ();
	}
	public void SwitchOff(){
		ButtonOff ();
	}

	private void ButtonOn (){
		if (!on) {
			on = true;
		}
	}


	private void ButtonOff(){
		if (on) {
			on = false;
		}
	}


}
