/*--------
 Program: mainmenuButtonController
 Author: Chok Chia Hsiang
 Date Created: 10th July 2018
 Description: Script to manage main menu button functions
 -------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtonController : MonoBehaviour {

	//***ACCESSED BY UI***

	//Goes to Level Select
	public void levelSelect() {
		
		SceneManager.LoadScene (1);

	}

	//Goes to options screen
	public void options(){

		SceneManager.LoadScene (2);

	}


	//Quits game
	public void quit(){

		Application.Quit();

	}

}
