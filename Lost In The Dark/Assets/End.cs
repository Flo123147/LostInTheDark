using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class End : MonoBehaviour {

	public int set, level;
	public bool lastLevel;
	// Use this for initialization
	void Awake () {

	}


	// Update is called once per frame
	void Update () {
	
	}

	public void Reload(){
	
		SceneManager.LoadScene ("level" + set + "-" + level);
	}

	public void LoadLevelSelect (){
		SceneManager.LoadScene ("00LevelSelect");
	}

	public void EndLevel(){
		string levelToLoad;
		if (lastLevel) {
			levelToLoad = "Level" + (set+1) + "-" + 0;
		} else {
			levelToLoad = "Level" + set + "-" + (level+1);
		}
		SceneManager.LoadScene (levelToLoad,LoadSceneMode.Single);
	}
}
