using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiRaceTimer : MonoBehaviour {

	public Text timerText;

	public float seconds; 
	public float minutes;


	// Use this for initialization
	void Start () {

		timerText = GetComponent<Text> () as Text;
		
	}
	
	// Update is called once per frame
	void Update () {
		minutes = (int)(Time.timeSinceLevelLoad / 60f);
		seconds = (int)(Time.timeSinceLevelLoad % 60f);
		timerText.text = "Timer : " + minutes.ToString("00") + ":" + seconds.ToString("00");
	}
}
