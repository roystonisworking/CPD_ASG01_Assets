using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager_MovingPlatforms : MonoBehaviour {

	// First Layer
	public Animation movingPlatformOut1R;
	public Animator movingPlatformOut1RAnimator;

	public Animation movingPlatformOut1L;
	public Animator movingPlatformOut1LAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// First Layer (Delay)
		if (!movingPlatformOut1RAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Moving Platform (2)_Out") &&
			!movingPlatformOut1LAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Moving Platform (1)_Out")) {
			
			StartCoroutine (changePositionsFirstLayer ());
		} 

		if (!movingPlatformOut1RAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Moving Platform (2)_In") &&
			!movingPlatformOut1LAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Moving Platform (1)_In")) {

			movingPlatformOut1LAnimator.SetBool ("MoveIn", false);
			movingPlatformOut1RAnimator.SetBool ("MoveIn", false);

		} 
	}

	// First Layer (Delay)
	IEnumerator changePositionsFirstLayer() {

		yield return new WaitForSeconds (7f);
		movingPlatformOut1LAnimator.SetBool ("MoveIn", true);
		movingPlatformOut1RAnimator.SetBool ("MoveIn", true);


	}
}
