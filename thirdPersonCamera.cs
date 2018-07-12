/*--------
 Program: thirdPersonCamera
 Author: Matthew Lau (edited by Chok Chia Hsiang)
 Date Created: 21 April 2018
 Date Last Modified: 24th June 2018
 Description: Script to manage camera
-------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonCamera : MonoBehaviour {


	public float mouseSensitivity = 10;

	private float verticalScroll;
	private float horizontalScroll;

	public Transform cameraPoint;
	public Transform player;
	Vector3 playerRotation;
	public float distanceFromPlayer = 3;

	public Vector2 verticalMinMax = new Vector2 (-20, 85);
	public Vector2 horizontalMinMax = new Vector2(-40,40);

	public float rotationSmoothTime = 0.12f;
	Vector3 rotationSmoothVelocity;
	Vector3 targetRotation;

	// Use this for initialization
	void Start () {
		
	}


	void LateUpdate() {

		// Assign mouse movement to floats
		horizontalScroll += Input.GetAxis ("Mouse X") * mouseSensitivity;
		verticalScroll -= Input.GetAxis ("Mouse Y") * mouseSensitivity;


		// Limit Camera scroll
		verticalScroll = Mathf.Clamp (verticalScroll, verticalMinMax.x, verticalMinMax.y);

		// Rotation derivative of value of floats
		 //playerRotation = Vector3.SmoothDamp (playerRotation, new Vector3 (verticalScroll, horizontalScroll), ref 
		// rotationSmoothVelocity,rotationSmoothTime);
		targetRotation = Vector3.SmoothDamp (targetRotation, new Vector3 (verticalScroll,horizontalScroll), ref rotationSmoothVelocity, rotationSmoothTime);


		transform.eulerAngles = targetRotation;

		// Camera positon relative of player
		transform.position = cameraPoint.position - (transform.forward * distanceFromPlayer) ;

		// Camera Rotation


	}
	

}
