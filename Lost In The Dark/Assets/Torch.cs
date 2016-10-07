using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour {


	public Animator anim;
	public bool isOn;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("IsOn", isOn);
	}
}
