using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationControl : MonoBehaviour {

	// Animators
	public Animator revisionRoomDoorAnimator;
	public Animator testChamberDoorAnimator;

	// Player
	public CharacterController controller;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {



	}

	// Check for Collision between Player and anything else
	void OnControllerColliderHit (ControllerColliderHit hit) {

		// Open Main Door
		if (hit.gameObject.name == "DoorOpener") {
			revisionRoomDoorAnimator.SetBool ("openDoorLeft", true);

			StartCoroutine (doorAutoClose());
		}

		// Open Door to Test Chamber
		if (hit.gameObject.name == "TestChamberDoorOpener") {
			testChamberDoorAnimator.SetBool ("openTestChamberDoor", true);

			StartCoroutine (testChamberDoorAutoClose ());
		}
	}

	// Revision room door auto close
	IEnumerator doorAutoClose() {
		yield return new WaitForSeconds (3f);
		revisionRoomDoorAnimator.SetBool ("openDoorLeft", false);

	}

	// Test chamber door auto close
	IEnumerator testChamberDoorAutoClose() {
		yield return new WaitForSeconds (3f);
		testChamberDoorAnimator.SetBool ("openTestChamberDoor", false);
	}
}
