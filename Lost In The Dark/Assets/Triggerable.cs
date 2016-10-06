using UnityEngine;
using System.Collections;

public interface Triggerable {

	void Trigger (Transform tr);
	void StayOn ();
	void SwitchOff();
}
