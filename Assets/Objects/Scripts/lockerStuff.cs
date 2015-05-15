using UnityEngine;
using System.Collections;

public class lockerStuff : MonoBehaviour {
	bool displayWords = false;
	public string lockerItem;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		displayWords = true;
	}
	
	void OnMouseUp() {
		displayWords = false;
	}
	
	void OnGUI() {
		if (displayWords) {
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-100), lockerItem);
		}
	}

}
