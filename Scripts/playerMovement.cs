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

	//*** All Float Values ***
	// Public Floats can be changed in Inspector
	// Private Floats should not need to be changed at all, but if need be can only be changed in Code

	//Ground Movement Values
	private float playerSpeed; //Player's speed placeholder
	private float walkSpeed; //Player's walk speed placeholder
	private float sprintSpeed; //Player's sprint/running speed placeholder
	public float baseWalkSpeed = 8.0f; //Player's default walk speed
	public float baseSprintSpeed = 15.0f; //Player's default sprint/running speed
	public float slowedWalkSpeed = 4.0f; //Player's walk speed when slowed by trap
	public float slowedSprintSpeed = 8.0f; //Player's run speed when slowed by trap

	public float wallRunningTime; 
	public float wallRunningSpeed;
	public float climbingSpeed = 0;

	//Jump Values
	public float gravity = 0.5f; 
	public float jumpForce; //Player's jump force placeholder
	public float baseJumpForce = 20.0f; //Player's default jump force
	public float wallJumpForce = 0.05f; //Player's jump force for wall jumping
	public float verticalVelocity; //Used to optimise jump

	public float manCannonForce = 50.0f; //Placeholder force for mancannon
	//Power up Values
	//Speed Boost
	public float boostTimer = 5.0f; //Duration of boosted speed
	public float boostSpeed = 30.0f; //Player's speed when boosted (overrides walk and sprint)
	//Jump Boost
	public float jumpBoostTimer = 5.0f; //Duration of boosted jump
	public float jumpBoostForce = 30.0f; //Player's jump force when boosted

	//Trap Values
	public float trappedTimer = 5.0f; //Duration of trap effects on player

	//Player rotation values
	public float verticalLookSensitivity = 5.0f;
	public float horizontalLookSensitivity = 5.0f;

	//public float dashDistance = 50.0f;



	// *** Booleans ***
	//private bool countdownStarted;//Placeholder
	private bool isfalling = false;  //Player is not grounded
	private bool canWallJump = false; //Player is in contact with a surface to wall jump off
	public bool isHoldingItem = false; //Player has rolled an item and has not used it
	public bool isBoosting = false; //Player is Speed Boosted
	public bool isSpringing = false; //Player is Jump Boosted
	private bool trapped = false; //Player is hit by a Slow Trap
	private bool stunned = false; //Player is hit by a Stun Trap
	//private bool canGlide = false;
	//private bool isGliding = false;
	private bool blastingOff; //Player is in contact with a ManCannon
	private bool isWallL = false; //Player is in contact with a wall-running wall from the left
	private bool isWallR = false; //Same as bove but from the right
	private bool canRunOnWall = false; //Player is confirmed to be in contact with a wall-running wall
	private bool runningOnWall = false; //Player is currently running on wall
	private bool isRunningOnWall = false;  //Secondary to help with managing momentum
	private bool canClimb = false; //Player is in contact with a wall-climb wall
	private bool isClimbing = false; //Player is currently climbing
	private bool exitClimbing = false; //Player has stopped climbing


	// Vector 3 for movement
	public Vector3 moveVector; //Basic movement direction
	private Vector3 lastMove; //to carry momentum when running and jumping, etc.

	private Vector3 mousePosition; //Checking mouse position

	private Vector3 climbUp; //Climbing direction.
	private Vector3 climbDown; //To be scrapped

	// Raycast for Wall Running
	private RaycastHit hitR; //Hit right side of player
	private RaycastHit hitL; //Hit left side of player

	public itemBoxRollItems itemRoll; //itemBoxRollClass to access

	// Use this for initialization
	void Start () {

		//Finding player character controller
		controller = GetComponent<CharacterController> ();

		//Hiding cursor
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		// Setting default movement values 
		walkSpeed = baseWalkSpeed;
		sprintSpeed = baseSprintSpeed;
		jumpForce = baseJumpForce;

	}

	void FixedUpdate(){

		//Player Movements
		playerMove ();

		wallRunning ();

		playerClimbing ();

		//Item interactions
		checkItem ();

		checkDebuffs();

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
		if (isBoosting) {
			
			playerSpeed = boostSpeed; //Applying boosted speed

		} else if (!isBoosting) {

			playerSpeed = baseWalkSpeed; //Resetting speed

		}

		// Jump Boosting------------------------------------------------------------------------
		if (isSpringing) {
			
			jumpForce = jumpBoostForce; //Applying boosted jump force

		} else if (!isSpringing) {

			jumpForce = baseJumpForce; //Resetting jump force

		}

		//Slowed/Stunned by traps-----------------------------------------------------------------------
		if (stunned) {

			//"Locking" movement when stunned 
			walkSpeed = 0;
			sprintSpeed = 0; 
			jumpForce = 0;

		} else if (trapped) {

			//Applying slowed speed
			walkSpeed = slowedWalkSpeed;
			sprintSpeed = slowedSprintSpeed;

		} else if (!stunned && !trapped) {

			//Resetting
			walkSpeed = baseWalkSpeed;
			sprintSpeed = baseSprintSpeed;
			jumpForce = baseJumpForce;

		}


		// Wall Running fast movement ----------------------------------------------------------

		if (runningOnWall == true) {
			
			playerSpeed = wallRunningSpeed; // Applying wall run speed

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

			canWallJump = true; //Able to walll jump

			//Wal jump
			if(Input.GetKeyDown(KeyCode.Space)){
	
				verticalVelocity = jumpForce * Time.deltaTime;
				moveVector.z = Input.GetAxis ("Vertical") * playerSpeed * Time.deltaTime;
				moveVector = hit.normal * wallJumpForce;

			}
		}
			

		// Check for Man Cannon Collision
		if (hit.gameObject.tag == "Man Cannon") {
			
			blastingOff = true; //Ready to man cannon jump

		} else {

			blastingOff = false; //Not ready to man cannon jump

		}

		// Check for Wall Climbing
		if (hit.gameObject.tag == "Ladder") {
			
			canClimb = true; //Player can initiate wall climb
			isClimbing = true; //Player (technically not) climbing

		} else {
			
			exitClimbing = true; //Player stopped climbing

		}

		if (hit.gameObject.tag == "Trap") {

			trapped = true; //Is hit by trap and effects
			Destroy (hit.gameObject); //Destroy trap

		}

		if (hit.gameObject.tag == "Stun Trap") {

			stunned = true;//Is hit by trap and effects
			Destroy (hit.gameObject); //Destroy trap

		}

	}


	// Wall Running ----------------------------------------------------------------

	private void wallRunning() {

		//Wall to the left
		if (Physics.Raycast (transform.position, -transform.right, out hitL, 1)) {
			Debug.DrawRay (transform.position, -transform.right * hitL.distance, Color.red, 2f);

			if (hitL.transform.tag == "Magnet Wall") {
				
				canRunOnWall = true; //Player can initiate wall running

				if (Input.GetKey (KeyCode.Q)) {
					
					isWallL = true;
					isWallR = false;
					runningOnWall = true; //Player is wall running 
					StartCoroutine (wallRunningLength ());

				}	

			} else {
				
				canRunOnWall = false; //Player cannot initiate wall running

			}


		//Wall to the right
		} else if (Physics.Raycast (transform.position, transform.right, out hitR, 1)) {
			Debug.DrawRay (transform.position, transform.right * hitR.distance, Color.red, 2f);

			if (hitR.transform.tag == "Magnet Wall") {
				
				canRunOnWall = true; //Player can initiate wall running

				if (Input.GetKey (KeyCode.Q)) {
					
					isWallL = false;
					isWallR = true;
					runningOnWall = true; //Player is wall running 
					StartCoroutine (wallRunningLength ());

				}

			} else {

				canRunOnWall = false; //Player cannot initiate wall running

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

		//Setting wall climb directions
		climbUp = new Vector3 (0, climbingSpeed, 0);
		climbDown = new Vector3 (0, -climbingSpeed, 0);

		if (canClimb == true) {
	
			if (Input.GetKey (KeyCode.R)) {
				isClimbing = true; //Player is climbing
				verticalVelocity = 0;
				climbingSpeed = 15;
				controller.transform.Translate (climbUp * Time.deltaTime);

			}

			if (Input.GetKeyUp (KeyCode.R)) {
				verticalVelocity -= gravity * Time.deltaTime;
				exitClimbing = true; //Player has stopped climbing

			}

			if (Input.GetKey (KeyCode.F)) {
				verticalVelocity = 0;
				climbingSpeed = 15;
				controller.transform.Translate (climbDown * Time.deltaTime);
			}

			if (Input.GetKeyUp (KeyCode.F)) {
				verticalVelocity -= gravity * Time.deltaTime;
				exitClimbing = true; //Player has stopped climbing

			}

			if (exitClimbing == true) {
				canClimb = false; //Player cannont initiate jump
				isClimbing = false; //Player has stopped climbing
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

			isBoosting = true; //Speed boost applied 
			StartCoroutine (StartCountdownSpeedBoost ()); //Initiate speed boost duration

		}

		// Check for Jump Booster after item box
		if (itemRoll.getJumpBoost == true) {

			isSpringing = true; //Jump boost applied
			StartCoroutine (StartCountdownJumpBoost ()); //Initiate jump boost duration
	
		}

	}


	//Boost durations

	IEnumerator StartCountdownSpeedBoost() {

		yield return new WaitForSeconds (boostTimer); //Waiting for boost duration to pass

		isBoosting = false; //Reset speed

		itemRoll.getSpeedBoost = false; //Item roll empty

	}

	IEnumerator StartCountdownJumpBoost() {

		yield return new WaitForSeconds (jumpBoostTimer); //Waiting for boost duration to pass

		isSpringing = false; //Reset jump force

		itemRoll.getJumpBoost = false; //Item roll empty

	}


//Debuff Effects-------------------------------------------------------------------------------

	void checkDebuffs(){

		if (trapped) {

			StartCoroutine (StartCountdownTrapped ()); //Start slow trap duration

		}
		if (stunned) {

			StartCoroutine (StartCountdownStunned ()); //Start stun trap duration

		}

	}


	//Debuff durations 
	IEnumerator StartCountdownTrapped() {

		yield return new WaitForSeconds (trappedTimer); //Waiting for debuff duration to pass

		trapped = false; //Reset speed

	}

	IEnumerator StartCountdownStunned() {

		yield return new WaitForSeconds (trappedTimer); //Waiting for debuff duration to pass

		stunned = false; //Reset movement values

	}
}
