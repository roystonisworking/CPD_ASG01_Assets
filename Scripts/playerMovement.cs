  	/*--------
 Program: playerMovement
 Author: Matthew Lau (edited by Chok Chia Hsiang)
 Date Created: 21 April 2018
 Date Last Modified: 24th June 2018
 Description: Script to manage movement input, base movements and interactions affecting movment.
-------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	//***VARIABLES***

	CharacterController controller;

	// All Float Values
	// Public Floats can be changed in Inspector
	// Private Floats should not need to be changed at all, but if need be can only be changed in Code

	private float walkSpeed;
	private float sprintSpeed;
	public float baseWalkSpeed = 8.0f;
	public float baseSprintSpeed = 15.0f;
	public float slowedWalkSpeed = 4.0f;
	public float slowedSprintSpeed = 8.0f;
	private float playerSpeed;

	public float gravity = 0.5f; 
	public float jumpForce;
	public float baseJumpForce = 20.0f;
	public float wallJumpForce = 0.05f;
	public float verticalVelocity;

	public float boostTimer = 5.0f;
	public float boostSpeed = 30.0f;

	public float jumpBoostTimer = 5.0f;
	public float jumpBoostForce = 30.0f;

	public float trappedTimer = 5.0f;

	public float verticalLookSensitivity = 5.0f;
	public float horizontalLookSensitivity = 5.0f;

	public float manCannonForce = 20.0f;
	public float dashDistance = 50.0f;

	public float wallRunningTime;
	public float wallRunningSpeed;
	public float climbingSpeed = 0;


	// Booleans

	private bool countdownStarted;
	private bool isfalling = false;
	private bool canWallJump = false;
	public bool isHoldingItem = false;
	public bool isBoosting = false;
	public bool isSpringing = false;
	private bool trapped = false;
	private bool stunned = false;
	private bool canGlide = false;
	private bool isGliding = false;
	private bool blastingOff;
	private bool isWallL = false;
	private bool isWallR = false;
	private bool canRunOnWall = false;
	private bool runningOnWall = false;
	private bool isRunningOnWall = false;
	private bool canClimb = false;
	private bool isClimbing = false;
	private bool exitClimbing = false;


	// Vector 3 for movement
	public Vector3 moveVector;
	private Vector3 lastMove;

	private Vector3 mousePosition;

	private Vector3 climbUp;
	private Vector3 climbDown;

	// Raycast for Wall Running
	private RaycastHit hitR;
	private RaycastHit hitL;

	public itemBoxRollItems itemRoll;

	// Use this for initialization
	void Start () {

		controller = GetComponent<CharacterController> ();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		// Setting base movement values 
		walkSpeed = baseWalkSpeed;
		sprintSpeed = baseSprintSpeed;
		jumpForce = baseJumpForce;

	}

	void FixedUpdate(){

		playerMove ();

		wallRunning ();

		playerClimbing ();

		checkItem ();

		checkDebuffs();

		Debug.Log (walkSpeed);
		Debug.Log (playerSpeed);
	}

//General Player Movement--------------------------------------------------------------------

	void playerMove() {

		// Moving using WASD -------------------------------------------------------------------
		playerSpeed = walkSpeed;		

		// Sprinting----------------------------------------------------------------------------
		if (Input.GetButton ("Sprint")) {
			
			playerSpeed = sprintSpeed;		

		}

		// Speed Boosting-----------------------------------------------------------------------
		if (isBoosting == true) {
			
			playerSpeed = boostSpeed;

		}

		// Jump Boosting------------------------------------------------------------------------
		if (isSpringing) {
			
			jumpForce = jumpBoostForce;

		} else if (!isSpringing) {

			jumpForce = baseJumpForce;

		}

		//Slowed/Stunned by traps-----------------------------------------------------------------------
		if (stunned) {

			walkSpeed = 0;
			sprintSpeed = 0; 

		} else if (trapped) {

			walkSpeed = slowedWalkSpeed;
			sprintSpeed = slowedSprintSpeed;

		} else if (!stunned && !trapped) {

			walkSpeed = baseWalkSpeed;
			sprintSpeed = baseSprintSpeed;

		}


		// Wall Running fast movement ----------------------------------------------------------

		if (runningOnWall == true) {
			
			playerSpeed = wallRunningSpeed;

		}

		//Assign movement to horizontal/vertical axisa(s)
		moveVector = Vector3.zero;
		moveVector.x = Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime;
		moveVector.z = Input.GetAxis ("Vertical") * playerSpeed * Time.deltaTime;

		//Movement based on camera
		moveVector = Camera.main.transform.TransformDirection (moveVector);


//		// Gliding-------------------------------------------------------------------------------
//
//		// Allow player input for gliding 
//		if (controller.isGrounded == false) {
//			
//			canGlide = true;
//
//		}
//
//		//Initiate gliding upon button input
//		if (canGlide == true && Input.GetKeyDown(KeyCode.G)) {
//
//			gravity = 0.2f;
//
//		}
			
	

		// Single Jump --------------------------------------------------------------------------
		if (controller.isGrounded) {

			// Reset gravity after glide
			gravity = 0.5f;

			// Gravity to stick Player to ground
			verticalVelocity = -gravity * Time.deltaTime;

			//Initiate jump upon button input
			if (Input.GetButtonDown ("Jump")) {

				verticalVelocity = jumpForce * Time.deltaTime;

			}
		
		//To prevent velocity of jump from carrying over to when player disengages from wall climbing and wall jumping
		} else if (canClimb == true || isClimbing == true || canRunOnWall == true || isRunningOnWall == true) {
			
			verticalVelocity -= gravity * Time.deltaTime;

		} else if (canWallJump == true && controller.isGrounded == false) {
			
			//Specifically for calculating trajectory of wall jump
			verticalVelocity -= gravity * Time.deltaTime;
			moveVector = lastMove;

		} else {
			
			// Gravity to pull player down
			verticalVelocity -= gravity * Time.deltaTime;

		}

		// Man Cannon -------------------------------------------------------------------------------
		if (blastingOff == true) {

			if (Input.GetKeyDown (KeyCode.Space)) {
				verticalVelocity = manCannonForce * Time.deltaTime;
				//moveVector.z = dashDistance * Time.deltaTime;
			}


		}

		// Wall Running --------------------------------------------------------------------------
		if (runningOnWall == true && !controller.isGrounded) {
			verticalVelocity = 0;
		}



		// Overall Movement
		moveVector.y = verticalVelocity;

		controller.Move (moveVector);
		lastMove = moveVector;


	}


	// Interaction with other objects -------------------------------------------------------------
	private void OnControllerColliderHit (ControllerColliderHit hit) {

		if (!controller.isGrounded && hit.normal.y < 0.1f && hit.gameObject.tag == "Wall") {

			canWallJump = true;
			
			if(Input.GetKeyDown(KeyCode.Space)){
	
			//Debug.DrawRay (hit.point, hit.normal, Color.red, 1.25f);
				verticalVelocity = jumpForce * Time.deltaTime;
				moveVector.z = Input.GetAxis ("Vertical") * playerSpeed * Time.deltaTime;
				moveVector = hit.normal * wallJumpForce;

			}
		}
			

		// Check for Man Cannon Collision
		if (hit.gameObject.tag == "Man Cannon") {
			
			blastingOff = true;

		} else {

			blastingOff = false;

		}

		// Check for Wall Climbing
		if (hit.gameObject.tag == "Ladder") {
			
			Debug.Log ("Climbing");
			canClimb = true;
			isClimbing = true;

		} else {
			
			exitClimbing = true;

		}

		if (hit.gameObject.tag == "Trap") {

			trapped = true;
			Destroy (hit.gameObject);

		}

		if (hit.gameObject.tag == "Stun Trap") {

			stunned = true;
			Destroy (hit.gameObject);

		}

	}


	// Wall Running ----------------------------------------------------------------

	private void wallRunning() {


		if (Physics.Raycast (transform.position, -transform.right, out hitL, 1)) {
			Debug.DrawRay (transform.position, -transform.right * hitL.distance, Color.red, 2f);

			if (hitL.transform.tag == "Magnet Wall") {
				
				canRunOnWall = true;

				if (Input.GetKey (KeyCode.Q)) {
					
					isWallL = true;
					isWallR = false;
					runningOnWall = true;
					StartCoroutine (wallRunningLength ());

				}	
			} else {
				canRunOnWall = false;
			}
		} else if (Physics.Raycast (transform.position, transform.right, out hitR, 1)) {
			Debug.DrawRay (transform.position, transform.right * hitR.distance, Color.red, 2f);

			if (hitR.transform.tag == "Magnet Wall") {
				
				canRunOnWall = true;

				if (Input.GetKey (KeyCode.Q)) {
					
					isWallL = false;
					isWallR = true;
					runningOnWall = true;
					StartCoroutine (wallRunningLength ());

				}
			} 
		}
	}

	IEnumerator wallRunningLength() {

		yield return new WaitForSeconds (wallRunningTime);

		isWallL = false;
		isWallR = false;
		runningOnWall = false;

	}

	// Wall Climbing ----------------------------------------------------------------

	private void playerClimbing() {

		climbUp = new Vector3 (0, climbingSpeed, 0);
		climbDown = new Vector3 (0, -climbingSpeed, 0);

		if (canClimb == true) {
	
			if (Input.GetKey (KeyCode.R)) {
				isClimbing = true;
				Debug.Log ("Up");
				verticalVelocity = 0;
				climbingSpeed = 15;
				controller.transform.Translate (climbUp * Time.deltaTime);

			}

			if (Input.GetKeyUp (KeyCode.R)) {
				verticalVelocity -= gravity * Time.deltaTime;
				exitClimbing = true;

			}

			if (Input.GetKey (KeyCode.F)) {
				Debug.Log ("Down");
				verticalVelocity = 0;
				climbingSpeed = 15;
				controller.transform.Translate (climbDown * Time.deltaTime);
			}

			if (Input.GetKeyUp (KeyCode.F)) {
				verticalVelocity -= gravity * Time.deltaTime;
				exitClimbing = true;

			}

			if (exitClimbing == true) {
				canClimb = false;
				isClimbing = false;
			}
		} 
	}

	IEnumerator wallClimbingLength() {

		yield return new WaitForSeconds (2f);
		isClimbing = false;

	}




//Item effects--------------------------------------------------------------------

	void checkItem(){
		
		// Check for Speed Booster after item box
		if (itemRoll.getSpeedBoost == true) {

			isBoosting = true;
			StartCoroutine (StartCountdownSpeedBoost ());

		}

		// Check for Jump Booster after item box
		if (itemRoll.getJumpBoost == true) {

			isSpringing = true;
			StartCoroutine (StartCountdownJumpBoost ());
	
		}

	}


	//Boost durations

	IEnumerator StartCountdownSpeedBoost() {

		yield return new WaitForSeconds (boostTimer);

		isBoosting = false;

		itemRoll.getSpeedBoost = false;

	}

	IEnumerator StartCountdownJumpBoost() {

		yield return new WaitForSeconds (jumpBoostTimer);

		isSpringing = false;

		itemRoll.getJumpBoost = false;

		Debug.Log ("stop jump");

	}


//Debuff Effects-------------------------------------------------------------------------------

	void checkDebuffs(){

		if (trapped) {

			StartCoroutine (StartCountdownTrapped ());
			Debug.Log (trapped);
		}
		if (stunned) {

			StartCoroutine (StartCountdownStunned ());

		}

	}


	//Debuff durations 
	IEnumerator StartCountdownTrapped() {

		yield return new WaitForSeconds (trappedTimer);

		Debug.Log ("ping");

		trapped = false;

	}

	IEnumerator StartCountdownStunned() {

		yield return new WaitForSeconds (trappedTimer);

		Debug.Log ("ping");

		stunned = false;

	}
}
