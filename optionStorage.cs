using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionStorage : MonoBehaviour {

	//***Vairables***
	//Volume
	public float volume = 1.0f;

	//Accessed across the entire game
	void Start (){
		
		DontDestroyOnLoad (this.gameObject);

	}

}
