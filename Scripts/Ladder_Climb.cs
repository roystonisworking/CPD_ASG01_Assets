using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_Climb : MonoBehaviour {

	private CharacterController characterController;
	private GameObject player;

	// Float Values ( Public floats can be changed in the Inspector. Private Floats should not be touched or changed to Public)
	private float verticalVelocity;
	private float gravity;
	public float climbingSpeed = 5.0f;

	// Booleans
	private bool isClimbing;


	// Use this for initialization
	void Start () {

		// Referencing from playerMovement Script
		characterController = GetComponent<CharacterController>();
		player = GameObject.Find ("MainPlayer");

		gravity = player.GetComponent<playerMovement>().gravity;
		verticalVelocity = player.GetComponent<playerMovement>().verticalVelocity;

	}

	// Update is called once per frame
	void Update () {

		Debug.Log ("isClimbing is" + " " + isClimbing);

		if (isClimbing == true) {

			// Make Player Climb
			verticalVelocity = climbingSpeed * Time.deltaTime;

			if (Input.GetKey(KeyCode.R)) {
				Debug.Log ("gravity" + verticalVelocity);

				Debug.Log ("Up");
				climbingSpeed = 100;

				//player.transform.Translate (Vector3.up * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.F)) {
				Debug.Log ("Down");
				climbingSpeed = -100;
				//player.transform.Translate (Vector3.down * Time.deltaTime);
			}


		}

		// Toggle back to normal gravity
		if (isClimbing == false) {
			
			verticalVelocity = -gravity * Time.deltaTime;

		}
		
	}


	void OnControllerColliderHit (ControllerColliderHit hit) {

		//if (!characterController.isGrounded && hit.normal.y < 0.1f && hit.gameObject.tag == "Wall") {

		// Climbing Ladders
		if (hit.gameObject.tag == "Ladder" && Input.GetKey (KeyCode.C)) {
			Debug.Log ("hit ladder");
			isClimbing = true;
			//Debug.Log ("gravity now" + verticalVelocity);
		} else if ( hit.gameObject.tag == null) {

			isClimbing = false;
		}




	}
}
