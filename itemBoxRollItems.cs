	/*--------
 Program: itemBoxRollItems
 Author: Matthew Lau (edited by Chok Chia Hsiang)
 Date Created: 21 April 2018
 Date Last Modified: 24th June 2018
 Description: Script to manage item box interactions and items rewarded to players
 -------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class itemBoxRollItems : MonoBehaviour {


	private int roll = 0;
	private float rollTimer = 3.0f;

	private bool rolling = false;
	public bool getSpeedBoost = false;
	public bool getJumpBoost = false;
	public bool getGrapplingHook = false;
	public bool getStunGun = false;
	public bool getTrap = false;

	public Text rollDisplay;

	private GameObject itemBox;

	public playerMovement playerStatus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		rollUIDisplay();
		
	}

	//UI displaying item stored
	void rollUIDisplay(){

		if (rolling && !playerStatus.isBoosting && !playerStatus.isSpringing && !getStunGun && !getTrap && !getGrapplingHook) {

			rollDisplay.text = "Item Roll:" + "Rolling";

		} else if (playerStatus.isBoosting) {
			
			rollDisplay.text = "Item Roll:" + "Speed Boosted";

		} else if (playerStatus.isSpringing) {

			rollDisplay.text = "Item Roll:" + "Jump Boosted";

		} else if (getStunGun) {

			rollDisplay.text = "Item Roll:" + "Stun Gun Get";

		} else if (getTrap) {

			rollDisplay.text = "Item Roll:" + "Trap Get";

		} else if (getGrapplingHook) {

			rollDisplay.text = "Item Roll:" + "Grappling Hook Get";
			
		} else {
			
			rollDisplay.text = "Item Roll:" + "Empty";

		}

	}

	//Roll items
	private void OnControllerColliderHit (ControllerColliderHit hit) {
		
		if (hit.gameObject.tag == "Item Box") {

			roll = Random.Range (1, 100);
			//Debug.Log (roll);

			itemBox = hit.gameObject;

			//cc IF player doesnt have item on him
			if (playerStatus.isHoldingItem == false || !rolling){
				
			//checkRoll ();
			StartCoroutine (startRoll ());
			rolling = true;
			Destroy (itemBox);
			playerStatus.isHoldingItem = true;

			}else if(playerStatus.isHoldingItem == true || rolling){

				Destroy (itemBox);
			
			}

		}
	}

	IEnumerator startRoll(){
		
		yield return new WaitForSeconds (rollTimer);

		rolling = false;
		checkRoll ();

	}

		//Award items based on roll
		void checkRoll(){
			
			//Grappling Hook (
			if (roll >= 1 && roll <=5){
				
				Debug.Log("Grappling Hook Get!");
				getGrapplingHook = true;

			//Stun Gun
			}if (roll >= 6 && roll <=20){
			
				Debug.Log("Stun Gun Get!");
				getStunGun = true;

			//Trap
			}if (roll >= 21 && roll <=35){
			
				Debug.Log("Trap Get!");
				getTrap = true;

			//Speed Boost
			}if (roll >= 36 && roll <=70){
			
				Debug.Log("Speed Boost Get!");
				getSpeedBoost = true;

			//Jump Boost
			}if (roll >= 71 && roll <=100){
			
				Debug.Log("Jump Boost Get!");
				getJumpBoost = true;

			}

		}
		
	}