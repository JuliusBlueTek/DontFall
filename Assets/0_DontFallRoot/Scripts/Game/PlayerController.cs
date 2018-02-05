using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerController : MonoBehaviour {

	[SerializeField] private HandController _leftHandController;
	[SerializeField] private HandController _RightHandControllerand;

	private Rigidbody _rigidbody;
	private Vector3 _lastTraversial = new Vector3(0,0,0);
	private bool _wasGripping = false;

#region Unity LifeCycle
	// Use this for initialization
	void Awake () {
		_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHands();
	}
#endregion

	void UpdateHands() {
		bool isGripping = false;

		if (_RightHandControllerand.IsGriping && _leftHandController.IsGriping) {
			// Solve for this
		}

		if (_RightHandControllerand.IsGriping) {
			GripAction(_RightHandControllerand);
			isGripping = true;
		}

		if (_leftHandController.IsGriping) {
			GripAction(_leftHandController);
			isGripping = true;
		}


		if(isGripping) { 
			_rigidbody.isKinematic = true;
			_wasGripping = true;
		} else {
			_rigidbody.isKinematic = false;
			if (_wasGripping) {
				_wasGripping = false;
				_rigidbody.velocity = _lastTraversial*100f;
			}
		}
	}

	void GripAction(HandController handController) {
		Vector3 controllerPosition = handController.GetControllerPosition();

		Vector3 traversal = controllerPosition - handController.transform.localPosition;

		transform.localPosition -= traversal;
		handController.transform.localPosition += traversal;

		_lastTraversial = traversal * -1;
	}
}
