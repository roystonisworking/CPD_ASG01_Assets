using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuAnimationControl : MonoBehaviour {

	public Animator tutorialDoorLeft;
	public Animator tutorialDoorRight;

	private CharacterController controller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		testingAnimations ();

	}

	void OnControllerColliderHit (ControllerColliderHit hit) {

		if (hit.gameObject.name == "DoorTriggerOne") {
			tutorialDoorLeft.SetBool ("Open Door", true);
			tutorialDoorRight.SetBool ("Open Door", true);

			StartCoroutine (tutorialDoorAutoClose ());

		}
		
	}

	IEnumerator tutorialDoorAutoClose() {

		yield return new WaitForSeconds (2f);
		tutorialDoorLeft.SetBool ("Open Door", false);
		tutorialDoorRight.SetBool ("Open Door", false);

	}

	void testingAnimations () {

		if (Input.GetKeyDown (KeyCode.Q)) {
			tutorialDoorLeft.SetBool ("Open Door", true);
			tutorialDoorRight.SetBool ("Open Door", true);

			StartCoroutine (tutorialDoorAutoClose ());

		}
	}
}
