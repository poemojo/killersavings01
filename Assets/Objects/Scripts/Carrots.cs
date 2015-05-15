using UnityEngine;
using System.Collections;

public class Carrots : MonoBehaviour {
	public string carrotWords;
	bool displayText = false;
	bool carrotTake = false;
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
				if (yesButtonClick == true && noButtonClick == false) {
				/**	if(Inventory.remainingActions >= 2){
						Inventory.remainingActions = Inventory.remainingActions - 2;
						if (Inventory.inventoryArray.Contains("carrots") == false) {
							Inventory.inventoryArray.Insert(Inventory.listCount, "carrots");
							Inventory.listCount++;
							carrotTake = true;
						}
					} */
				}
				executedTime = 0.0f;
				//yesButtonClick = false;
				noButtonClick = false;
				
			}
		}
		if (carrotTake) {
			Destroy(gameObject);
		}
	}
	
	void OnMouseDown() {
		displayText = true;
		clickCount++;;
	}
	
	void OnMouseUp() {
		if (clickCount % 2 == 0) {
			displayText = false;
		}
	}
	
	void OnGUI() {
		if (displayText) {
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), carrotWords + " Should you take them?");
			if (GUI.Button (new Rect (20,40,220,20), "Sure, why not!")){
				yesButtonClick = true;
				executedTime = Time.time;
			}
			if (GUI.Button (new Rect (20,80,250,20), "I do not carrot all about those carrots.")){
				noButtonClick = true;
				executedTime = Time.time;
			}
		}
		if (yesButtonClick) {
			displayText = false;
			
		}
		if (noButtonClick) {
			displayText = false;
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), "Better leave 'em be, then!");
			
		}
	}
	
}
