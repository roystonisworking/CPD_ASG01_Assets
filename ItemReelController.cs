using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReelController : MonoBehaviour {

	public Transform reelBottomEnd;
	public Transform reelTopEnd;
	public Transform reelItemSetList;

	private Vector3 reelBottomEndPosition;
	private Vector3 reelTopEndPosition;

	public float reelSpeed = 0.0f;
	public float reelSpeedDecrease = 500.0f;
	public float currentReelSpeed;

	//public bool startReel;

	// Use this for initialization
	void Start () {

		Debug.Log (reelSpeed);
		//Debug.Log("Child Objects: " + CountChildren(transform));
		reelBottomEndPosition = reelBottomEnd.position;
		reelTopEndPosition = reelTopEnd.position;

	}
		

	// Update is called once per frame
	void Update () {

		Spinning ();
		if (reelSpeed > 0.0f) {
			reelSpeed -= reelSpeedDecrease * Time.deltaTime * 1;
		}
		if (reelSpeed <= 0.0f) {
			reelSpeed = 0;
		}
		checkSpinInput ();

	}

	//Check input to start spinning reel
	void checkSpinInput () {

		if (Input.GetKeyDown (KeyCode.L)) {

			reelSpeed = 1000.0f;
			//Spinning ();

		}
	}

//	//Duration of which the item reel rolls
//	void StartSpinDuration() {
//
//		reelTimer = 5.0f;
//		Spinning();
//	}
//

	//Spin the reel...kinda
	void Spinning()
	{
		currentReelSpeed = reelSpeed * Time.deltaTime;
		reelItemSetList.Translate(Vector3.down * currentReelSpeed);

		foreach (Transform child in reelItemSetList.transform){
			if (child.position.y <= reelBottomEndPosition.y){
				child.position = reelTopEndPosition;
			}	
		}
	}
}
