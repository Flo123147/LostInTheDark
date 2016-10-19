using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour, DoesNotBlock{

	public TorchSpawn[] spawnPoints;

	private float spawnChance = 0.5f;
	// Use this for initialization
	void Awake () {
		if (spawnPoints.Length >= 1 && Random.value >= 1 - spawnChance) {
			int rInt = Random.Range (0, spawnPoints.Length );
			spawnPoints [rInt].SpawnTorch (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isBlocking (){
		return true;
	}

}