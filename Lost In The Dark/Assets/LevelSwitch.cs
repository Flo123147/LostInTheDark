using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSwitch : MonoBehaviour {

	public InputField inputField;


	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel(){
		string str = "Level" + inputField.text;
		
		SceneManager.LoadScene (str);
	}
}
