	/*--------
 Program: itemBoxRollItems
 Author: Chok Chia Hsiang
 Date Created: 21 April 2018
 Date Last Modified: 15th July 2018
 Description: Script to manage item box interactions and items rewarded to players
 -------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class itemBoxRollItems : MonoBehaviour {

    //**VARIABLES**

    //"Spin the wheel"
	private int roll = 0; //Determines what item will be given
	private float rollTimer = 3.0f; //Delay of which item will be rolled and given
	private bool rolling = false; //Wether item is being rolled or not
    
	public bool getSpeedBoost = false;//Wether you got the Speed Boost or not
	public bool getJumpBoost = false;//Wether you got the Jump Boost or not
	public bool getGrapplingHook = false;//Wether you got the Grappling Hook or not
	public bool getStunTrap = false;//Wether you get the Stun Trap or not
    public bool getSlowTrap = false;//Wether you get the Slow Trap or not

	public Text rollDisplay;//UI text for diplaying what powerup is held

	private GameObject itemBox;//Item box GameObject

	public playerMovement playerStatus;//playerMovement class called.

	
	// Update is called once per frame
	void Update () {

		rollUIDisplay(); // Updating UI 
		
	}

	//UI displaying item stored
	void rollUIDisplay(){

		if (rolling && !playerStatus.isBoosting && !playerStatus.isSpringing && !getStunTrap && !getSlowTrap && !getGrapplingHook) {

			rollDisplay.text = "Item Roll:" + "Rolling";

		} else if (playerStatus.isBoosting) {
			
			rollDisplay.text = "Item Roll:" + "Speed Boosted";

		} else if (playerStatus.isSpringing) {

			rollDisplay.text = "Item Roll:" + "Jump Boosted";

		} else if (getStunTrap) {

			rollDisplay.text = "Item Roll:" + "Stun Trap Get";

		} else if (getSlowTrap) {

			rollDisplay.text = "Item Roll:" + "Slow Trap Get";

		} else if (getGrapplingHook) {

			rollDisplay.text = "Item Roll:" + "Grappling Hook Get";
			
		} else {
			
			rollDisplay.text = "Item Roll:" + "Empty";

		}

	}

	//Roll items
	private void OnControllerColliderHit (ControllerColliderHit hit) {
		
        //IF player hits an item box
		if (hit.gameObject.tag == "Item Box") {

			roll = Random.Range (1, 100); //Determing item awarded

			itemBox = hit.gameObject; //Assigning item box under itemBox GameObject in this script (Before being destroyed)

			//IF player doesnt have item on him or is rolling
			if (playerStatus.isHoldingItem == false || !rolling){
				
			StartCoroutine (startRoll ()); //Initiate rolling
			rolling = true; //Is currently rolling
			Destroy (itemBox); //Destroying the item box
			playerStatus.isHoldingItem = true;//Player is holding an item (Technically an unknown item since not rolled yet.
            
            //IF player does have an item or is rolling
			}else if(playerStatus.isHoldingItem == true || rolling){

				Destroy (itemBox); //Destroying the item box only
			
			}

		}
	}

    //Rolling for itmes
	IEnumerator startRoll(){
		
		yield return new WaitForSeconds (rollTimer); //Delayed before actual roll

		rolling = false; //Not rolling for items
		checkRoll (); //Award items after roll

	}

		//Award items based on roll
		void checkRoll(){
			
			//Rolled Grappling Hook 
			if (roll >= 1 && roll <=5){
				
				getGrapplingHook = true; 

			//Rolled Stun Trap
			}if (roll >= 6 && roll <=20){
		
				getStunTrap = true;

			//Rolled Slow Trap
			}if (roll >= 21 && roll <=35){
			
                getSlowTrap = true;

			//Rolled Speed Boost
			}if (roll >= 36 && roll <=70){
			
				getSpeedBoost = true;

			//Rolled Jump Boost
			}if (roll >= 71 && roll <=100){
			
				getJumpBoost = true;

			}

		}
		
	}