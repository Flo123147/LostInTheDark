using UnityEngine;
using System.Collections;

public class TorchSpawn : MonoBehaviour {

	public Transform spawnOn;
	public Transform torchOn, torchOff;

	// Use this for initialization
	void Awake () {
		spawnOn.transform.GetComponent<SpriteRenderer> ().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnTorch(bool isOn){

		Transform torch_Go  = null;

		if (isOn) {
			torch_Go = (Transform)Instantiate (torchOn, spawnOn.position + new Vector3(0,0,torchOff.transform.position.z), spawnOn.rotation);
		} else {
			torch_Go = (Transform)Instantiate (torchOff, spawnOn.position + new Vector3(0,0,torchOff.transform.position.z), spawnOn.rotation);
		}

		Torch torch = torch_Go.GetComponent<Torch> ();
		torch.anim.SetFloat ("Speed", (0.3f*Random.value) + 1);

	}
}
