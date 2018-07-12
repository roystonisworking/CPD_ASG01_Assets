using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class levelSelectScrollController : MonoBehaviour {

	public RectTransform levelScrollList;
	public RectTransform anchor;

	public Vector3 scrollPos;
	public Vector3 anchorPos;
	// Update is called once per frame
	void Update () {
		
		scrollPos = levelScrollList.transform.position;
		anchorPos = anchor.transform.position;
		//scrollPos.x = EventSystem.current.currentSelectedGameObject.transform.position.x;
		scrollPos.x = scrollPos.x - (EventSystem.current.currentSelectedGameObject.transform.position.x - anchorPos.x);
		//EventSystem.current.currentSelectedGameObject.transform.position = scrollPos;

		levelScrollList.transform.position = scrollPos;

	}

}
