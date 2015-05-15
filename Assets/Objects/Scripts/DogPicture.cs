using UnityEngine;
using System.Collections;

public class DogPicture : MonoBehaviour {
	public string dogWords;
	bool displayText = false;
	public bool yesButtonClick = false;
	bool noButtonClick = false;
	private float currentTime = 0.0f, executedTime = 0.0f, timeToWait = 2.0f;
	int clickCount = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentTime = Time.time;
		
		if(executedTime != 0.0f)
		{
			if(currentTime - executedTime > timeToWait)
			{
				//if (yesButtonClick == true && noButtonClick == false) {
				//Destroy(gameObject);
				//}
				executedTime = 0.0f;
				yesButtonClick = false;
				noButtonClick = false;
				
			}
		} 
	
	}

	void OnMouseDown() {
		displayText = true;
		clickCount++;
	}
	
	void OnMouseUp() {
		if (clickCount % 2 == 0) {
			displayText = false;
		}
	}
	
	void OnGUI() {
		if (displayText) {
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), dogWords + " Should you take it?");
			if (GUI.Button (new Rect (20,40,220,20), "I love dogs!")){
				yesButtonClick = true;
				executedTime = Time.time;
			}
			if (GUI.Button (new Rect (20,80,200,20), "I hate dogs.")){
				noButtonClick = true;
				executedTime = Time.time;
			}
		}
		if (yesButtonClick) {
			displayText = false;
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), "Your picture now!");
			/**if (Inventory.inventoryArray.Contains("dog picture") == false) {
				Inventory.inventoryArray.Insert(Inventory.listCount, "dog picture");
				Inventory.listCount++;
			} */
			
		}
		if (noButtonClick) {
			displayText = false;
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), "You are a fine person.");
			
		}
	}

}
