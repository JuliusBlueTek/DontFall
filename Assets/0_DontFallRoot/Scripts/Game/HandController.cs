using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class HandController : MonoBehaviour {

	[SerializeField] private OVRInput.Controller _Controller;
	[SerializeField] private Transform _MetacarpalTransform;
	[SerializeField] private Transform _ProximalTransform;
	[SerializeField] private Transform _IntermediateTransform;


	#region unity loop
	// Update is called once per frame
	void Update () {
		VrUpdateTransform();
	}
	#endregion


	void VrUpdateTransform() {
		_MetacarpalTransform.position =  OVRInput.GetLocalControllerPosition(_Controller);
		_MetacarpalTransform.rotation = OVRInput.GetLocalControllerRotation(_Controller);
	}
}
