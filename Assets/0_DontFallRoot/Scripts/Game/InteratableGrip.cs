using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteratableGrip : MonoBehaviour {

#region unity loop 

	void OnTriggerEnter(Collider collider) {
		if (collider.attachedRigidbody.gameObject.tag == "Grip") {
			HandController handController = collider.attachedRigidbody.gameObject.GetComponent<HandController>();

			handController._canGrip = true;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.attachedRigidbody.gameObject.tag == "Grip")
		{
			HandController handController = collider.attachedRigidbody.gameObject.GetComponent<HandController>();

			handController._canGrip = false;
		}
	}
	#endregion
}
