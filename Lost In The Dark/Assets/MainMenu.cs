using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Text text;
	private string startText;

	// Use this for initialization
	void Awake () {
		startText = text.text;
		string s = PlayerPrefs.GetString ("lastLevel");
		s = s.Substring (5);
		text.text = (startText + " (Last Level:" + s + ")");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void StartGame(){
		try{
			SceneManager.LoadScene (PlayerPrefs.GetString ("lastLevel"));

		}catch{
			SceneManager.LoadScene ("Level1-0");

		}

	}

	public void ResetCounter(){
		PlayerPrefs.SetString ("lastLevel","level1-0");

	
		text.text = (startText + " (Last Level:" + "" + 1 + "-" + 0 + ")");
	}

	public void LevelSelect(){
		SceneManager.LoadScene ("00LevelSelect");
	}

	public void Quit(){
		Application.Quit ();
	}
}
