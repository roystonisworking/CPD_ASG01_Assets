using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager_DisappearingPlatforms : MonoBehaviour {

	public GameObject platformOne;
	public GameObject platformTwo;
	public bool poof = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (poof == false) {
			StartCoroutine (platformDisappear ());
		}

		if (poof == true) {
			StartCoroutine (platformAppear ());
		}
	}

	IEnumerator platformDisappear () {

		yield return new WaitForSeconds (7f);
		platformOne.SetActive(false);
		platformTwo.SetActive(true);
		poof = true;
	}

	IEnumerator platformAppear () {

		yield return new WaitForSeconds (7f);
		platformOne.SetActive(true);
		platformTwo.SetActive(false);
		poof = false;
	}
}
