using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour {

#region Unity LifeCycle
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch); 
	}
#endregion

}
