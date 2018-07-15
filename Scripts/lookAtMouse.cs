/*--------
 Program: lookAtMouse
 Author: Chok Chia Hsiang
 Date Created: 21 April 2018
 Date Edited: 15th July 2018
 Description: Script to get button to get player to face where the mouse is
 -------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtMouse : MonoBehaviour {


	//***VARIABLES

//	Vector3 positionOnScreen;
//	Vector3 mouseOnScreen;
//	Vector3 forward;


	private Vector3 worldPos; //cooridinates of the camera based on the world.

	public GameObject mainPlayer; //main character GameObject

	private float mouseX; //mouseX Input Axis (horizontal mouse movement)
	private float mouseY; //mouseY Input Axis (vertical mouse movement)   
    private float cameraDif; //Difference between camera and player in height (y-axis)




	// Use this for initialization
	void Start () {

		cameraDif = Camera.main.transform.position.y - mainPlayer.transform.position.y; //calculating difference in elevation

	}
	
	// Update is called once per frame
	void Update () {

		mouseX = Input.mousePosition.x; //Assigning mouseX input

		mouseY = Input.mousePosition.y; //Assigning mouseY input

        worldPos = Camera.main.ScreenToWorldPoint( new Vector3( mouseX, mouseY, cameraDif)); //Assigning camera position

		Vector3 lookDirection = new Vector3 (worldPos.x, mainPlayer.transform.position.y, worldPos.z); //direction of camera

		mainPlayer.transform.LookAt (lookDirection); //turning player around to the same direction as camera.


		//In case if needed

//		mouseOnScreen = Input.mousePosition;
//
//		Debug.Log ("mousey" + mouseOnScreen);
//
//		positionOnScreen = Camera.main.ScreenToWorldPoint(new Vector3 (mouseOnScreen.x, 0, mainPlayer.transform.position.y));
//
//		forward = positionOnScreen - mainPlayer.transform.position;
//
//		mainPlayer.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

	}


}
	
