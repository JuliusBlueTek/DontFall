using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class HandController : MonoBehaviour {

	[SerializeField] private LayerMask _gripLayerMaks;
	[SerializeField] private OVRInput.Controller _Controller;
	[SerializeField] private Transform _metacarpalTransform;
	[SerializeField] private Transform _proximalTransform;
	[SerializeField] private Transform _intermediateTransform;

	private bool _isGriping = false;
	public bool IsGriping {
		get { return _isGriping; }
	} 

	[SerializeField]
	public bool _canGrip = false;

	private const float VelocityMagic = 6000f;
	private const float AngularVelocityMagic = 50f;
	private const float MaxVelocityChange = 10f;
	private const float MaxAngularVelocityChange = 20f;

	private Rigidbody _rigidbody;


	#region unity loop
	void Awake() {
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update() {
		GetInput();
	}

	// Update is called once per frame
	void FixedUpdate() {

			VrUpdateForce();
	}

	void OnCollisionEnter(Collision collision) {
	}

	void OnCollisionExit(Collision collision) {
	}
	#endregion

	public Vector3 GetControllerPosition() {
		return OVRInput.GetLocalControllerPosition(_Controller);
	}

	void GetInput() {
		if (_canGrip) {
			if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, _Controller) > 0.5f) {
				_isGriping = true;
			}
		}

		if (_isGriping) {
			if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, _Controller) < 0.5f)
			{
				_isGriping = false;
			}
		}
	}

	void VrUpdateForce() {

		if (IsGriping) {
			_rigidbody.isKinematic = true;
		} else {
			_rigidbody.isKinematic = false;
		}


		Vector3 controllerPosition =  OVRInput.GetLocalControllerPosition(_Controller);
		Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(_Controller);

		Vector3 directionVector = controllerPosition - transform.localPosition;

		_rigidbody.AddForce(directionVector*5f);

		float velocityMagic = VelocityMagic / (Time.deltaTime / 0.0111f);
		float angularVelocityMagic = AngularVelocityMagic / (Time.deltaTime / 0.0111f);

		Vector3 positionDelta;
		Quaternion rotationDelta;

		float angle;
		Vector3 axis;

		positionDelta = controllerPosition - transform.localPosition;
		rotationDelta = controllerRotation * Quaternion.Inverse(transform.localRotation);


		Vector3 velocityTarget = (positionDelta * velocityMagic) * Time.deltaTime;
		if (float.IsNaN(velocityTarget.x) == false)
		{
			_rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity, velocityTarget, MaxVelocityChange);
		}


		rotationDelta.ToAngleAxis(out angle, out axis);

		if (angle > 180)
			angle -= 360;

		if (angle != 0)
		{
			Vector3 angularTarget = angle * axis;
			if (float.IsNaN(angularTarget.x) == false)
			{
				angularTarget = (angularTarget * angularVelocityMagic) * Time.deltaTime;
				_rigidbody.angularVelocity = Vector3.MoveTowards(_rigidbody.angularVelocity, angularTarget, MaxAngularVelocityChange);
			}
		}

	}
}
