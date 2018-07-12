using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtMouse : MonoBehaviour {

//	Vector3 positionOnScreen;
//	Vector3 mouseOnScreen;
//	Vector3 forward;
//

	private Vector3 worldPos;

	public GameObject mainPlayer;

	private float mouseX;
	private float mouseY;
	private float cameraDif;




	// Use this for initialization
	void Start () {

		cameraDif = Camera.main.transform.position.y - mainPlayer.transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {

		mouseX = Input.mousePosition.x;

		mouseY = Input.mousePosition.y;

		worldPos = Camera.main.ScreenToWorldPoint( new Vector3( mouseX, mouseY, cameraDif));

		Vector3 lookDirection = new Vector3 (worldPos.x, mainPlayer.transform.position.y, worldPos.z);

		mainPlayer.transform.LookAt (lookDirection);


//		mouseOnScreen = Input.mousePosition;
//
//		Debug.Log ("mousey" + mouseOnScreen);
//
//		positionOnScreen = Camera.main.ScreenToWorldPoint(new Vector3 (mouseOnScreen.x, 0, mainPlayer.transform.position.y));
//
//		forward = positionOnScreen - mainPlayer.transform.position;
//
//		mainPlayer.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
//
	}


}
	
