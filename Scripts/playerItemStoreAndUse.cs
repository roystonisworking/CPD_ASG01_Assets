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

public class playerItemStoreAndUse : MonoBehaviour {


	public Rigidbody playerRigidbody;
	public Transform cam;
	private RaycastHit hit;
	private bool hooked;
	public GameObject hookObject;
	public float momentum;
	public float speed = 5;
	public float step;

	public Transform itemSpawn;

	public GameObject grapplePrefab;
	public GameObject trapPrefab;
	public GameObject stunTrapPrefab;

	public itemBoxRollItems itemRoll;

	// Use this for initialization
	void Start () {

		playerRigidbody = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {

		//grappleFire ();
		trapFire ();
		stunGunFire ();

	}

//	void grappleFire(){
//
//		if (itemRoll.getGrapplingHook == true) {
//
//			if (Input.GetKeyDown (KeyCode.E)) {
//
//				//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
////				if (Physics.Raycast (cam.position, cam.forward, out hit)){
////
////					hooked = true;
////					playerRigidbody.isKinematic = true;
////
////				}
//
//				
//				var hook = (GameObject)Instantiate (grapplePrefab, itemSpawn.position, itemSpawn.rotation);
//
//				hook.GetComponent<Rigidbody> ().velocity = (hook.transform.forward * 50) + (hook.transform.up * 2); 
//
//				itemRoll.getGrapplingHook = false;
//
//				hookObject = hook;
//
//			}
//
//		}
//
//
//		if (hooked){
//
//			momentum += Time.deltaTime * speed;
//			step = momentum * Time.deltaTime;
//			transform.position = Vector3.MoveTowards(transform.position, hookObject.transform.position , step);
//
//
//			if (Input.GetKeyDown (KeyCode.E)) {
//
//				Destroy (hookObject);
//			}
//		}
//			
//			
//	}

	void trapFire(){

		if (itemRoll.getTrap == true) {

			if (Input.GetKeyDown (KeyCode.E)) {

				var trap = (GameObject)Instantiate (trapPrefab, itemSpawn.position, itemSpawn.rotation);

				trap.GetComponent<Rigidbody>().velocity = trap.transform.forward * 2; 

				itemRoll.getTrap = false;

			}
		}

	}

	void stunGunFire(){

		if (itemRoll.getStunGun == true) {

			if (Input.GetKeyDown (KeyCode.E)) {

				var trap = (GameObject)Instantiate (stunTrapPrefab, itemSpawn.position, itemSpawn.rotation);

				trap.GetComponent<Rigidbody>().velocity = trap.transform.forward * 20; 

				itemRoll.getStunGun = false;

			}
		}

	}

}
