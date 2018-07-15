/*--------
 Program: levelSelectController
 Author: Chok Chia Hsiang
 Date Created: 10th July 2018
 Last Edited: 15th July 2018
 Description: Script to manage level select menu and button scrolling/placements
 -------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class levelSelectScrollController : MonoBehaviour {

    //**VARIABLES**

    //Transforms and coordinates
	public RectTransform levelScrollList; //The actual set of buttons
	public RectTransform anchor; //Placeholder on the middle of the screen/where the buttons are supposed to be.
	public Vector3 scrollPos; //Cooridnates of the set of buttons
	public Vector3 anchorPos; //Coordidnates of the anchor


	//Keeping the selected button centered.
	void Update () {
		
		scrollPos = levelScrollList.transform.position; //Assign coordiantes of the button set
		anchorPos = anchor.transform.position; //Assign cooridantes of the anchor

	
		scrollPos.x = scrollPos.x - (EventSystem.current.currentSelectedGameObject.transform.position.x - anchorPos.x); //Calculate new coordinates based on distance between selected button and anchor.
        //scrollPos.x = EventSystem.current.currentSelectedGameObject.transform.position.x; 
        //EventSystem.current.currentSelectedGameObject.transform.position = scrollPos;

        levelScrollList.transform.position = scrollPos; //Change button set's x-cooridates to new coordinates

	}

}
