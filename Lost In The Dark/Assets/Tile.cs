using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {



	private SpriteRenderer sr;
	private float alpha;

	private float lerpTo,currLerp;

	private bool isActive;

	private float fadeSpeed;
	// Use this for initialization
	void Awake () {		
		sr = GetComponent<SpriteRenderer> ();
		sr.enabled = true;
		sr.sortingOrder = 1;
		alpha = 1;
		lerpTo = 1;
		currLerp = 1;
		fadeSpeed = 2;
	}
	
	// Update is called once per frame
	void Update () {

		if (currLerp != lerpTo) {
			isActive = true;
	
			if (currLerp < lerpTo) {
				currLerp += Time.deltaTime/fadeSpeed;

				if (currLerp >= lerpTo)
					currLerp = lerpTo;

			} else {
				currLerp -= Time.deltaTime;

				if (currLerp <= lerpTo)
					currLerp = lerpTo;			
			}

			alpha = Mathf.Lerp (0, 1, currLerp);

			sr.color = new Color (0, 0, 0, alpha);
		} else {
			isActive = false;
		}

	}

	public bool isTileActive(){
		return isActive;
	}

	public void Vis(bool open ,float dist, float maxDist){
		if (open) {
			
			lerpTo = (dist / maxDist);
		
		
		} else {
			lerpTo = 1;
		}
	}


}