using UnityEngine;
using System.Collections;

public class Fridge : MonoBehaviour {

	public string fridgeWords; 
	bool displayText = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		displayText = true;
	}

	void OnMouseUp() {
		displayText = false;
	}

	void OnGUI() {
		if (displayText) {
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-100), "Fridge: " + fridgeWords);
		}
	}

}
