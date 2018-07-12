using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManCannon : MonoBehaviour {

	Vector3 launch = Vector3.zero;

	public float forwardPropulsion = 5.0f;
	public float upwardPropulsion = 5.0f;

	public CharacterController controller;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnControllerColliderHit (ControllerColliderHit colBoost) {

		if (colBoost.gameObject.tag == "Man Cannon" && Input.GetKey (KeyCode.Space)) {

			Debug.Log ("launch");


		}
	}
}
