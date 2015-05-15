using UnityEngine;
using System.Collections;

public class Earring : MonoBehaviour {
	public string earringWords;
	bool displayText = false;
	bool earringTake = false;
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
					/**if(Inventory.remainingActions >= 2){
						Inventory.remainingActions = Inventory.remainingActions - 2;
						if (Inventory.inventoryArray.Contains("earring") == false) {
							Inventory.inventoryArray.Insert(Inventory.listCount, "earring");
							Inventory.listCount++;
							earringTake = true;
						}
					} */
				}
				executedTime = 0.0f;
				yesButtonClick = false;
				noButtonClick = false;
				
			}
		}
		if (earringTake) {
			Destroy(gameObject);
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
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), earringWords + " Should you take it?");
			if (GUI.Button (new Rect (20,40,220,20), "Sure, why not!")){
				yesButtonClick = true;
				executedTime = Time.time;
			}
			if (GUI.Button (new Rect (20,80,200,20), "It's not my color.")){
				noButtonClick = true;
				executedTime = Time.time;
			}
		}
		if (yesButtonClick) {
			displayText = false;
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), "You could probably get a dollar for it on eBay.");
			/**if (Inventory.inventoryArray.Contains("earring") == false) {
				Inventory.inventoryArray.Insert(Inventory.listCount, "earring");
				Inventory.listCount++;
			}*/
			
		}
		if (noButtonClick) {
			displayText = false;
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), "You are a fine person.");
			
		}
	}
	
}