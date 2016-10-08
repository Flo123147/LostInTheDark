using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	public TorchSpawn[] spawnPoints;

	private float spawnChance = 0.5f;
	// Use this for initialization
	void Awake () {
		if (Random.value >= 1 - spawnChance) {
			int rInt = Random.Range (0, spawnPoints.Length );
			spawnPoints [rInt].SpawnTorch (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}