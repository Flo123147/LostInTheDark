using UnityEngine;
using System.Collections;

public class Start : MonoBehaviour {

	public Transform playerPrefab;
	public SpriteRenderer sr;
	public int playerLightLevel;

	// Use this for initialization
	void Awake () {
		
		Transform tr =(Transform) Instantiate (playerPrefab, transform.position, new Quaternion());

		tr.GetComponent<Player> ().maxLightLengt = playerLightLevel * playerLightLevel;
		sr.enabled = false;

	} 
	
	// Update is called once per frame
	void Update () {
	
	}
}
