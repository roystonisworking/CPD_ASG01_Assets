/*--------
Program: uiRaceTimer
Author: Chok Chia Hsiang
Date Created: 21 April 2018
Date Last Modified: 15th July 2018
Description: Script to manage item box interactions and items rewarded to players
-------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiRaceTimer : MonoBehaviour {

	public Text timerText; //UI Text for displaying timer

	public float seconds; //float for seconds
	public float minutes; //floar for minutes


	// Use this for initialization
	void Start () {

		timerText = GetComponent<Text> () as Text; //Assign UI as timerText
		
	}
	
	// Update is called once per frame
	void Update () {
		minutes = (int)(Time.timeSinceLevelLoad / 60f); //float increasing at the rate of a minute
		seconds = (int)(Time.timeSinceLevelLoad % 60f); //float increasing at the rate of a second
		timerText.text = "Timer : " + minutes.ToString("00") + ":" + seconds.ToString("00"); //updating UI to timer
	}
}
