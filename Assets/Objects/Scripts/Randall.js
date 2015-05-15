#pragma strict

private var npcWords: String[];
var presentEvidence : boolean[];
var evidence = false; 
var cashWad = false; 
var dogPicture = false; 
var displayText = false; 
var present = false;
var dialog = false;
var NPCLike = false;
var randomNum: int = 0;
private var currentTime = 0.0f;
private var executedTime = 0.0f;
private var timeToWait = 2.0f;
private var currentTime1 = 0.0f;
private var executedTime1 = 0.0f;
private var timeToWait1 = 4.0f;
var clickCount: int = 0;
var talkCounter: int = 0;
var likedWordCount: int = 0;
var dislikedWordCount: int = 0;
var randallCam: Camera;
var playerCam: Camera;
var controller: Animator;

// Use this for initialization
function Start () {
	npcWords = new String[6];
	presentEvidence = new boolean[10];
	npcWords[0] = "I really like parkour.";
	npcWords[1] = "Veronica’s cute and all, but I could never like a girl who loves vegetables.";
	npcWords[2] = "Randall’s got some nerve saying his dog is better than my cats.";
	npcWords[3] = "I did it...what, you think I'm kidding?";
	npcWords[4] = "I heard Veronica got into a heated argument with a customer the other day.";
	npcWords[5] = "Randall doesn’t trust the banks.";
}

// Update is called once per frame
function Update () {
	currentTime = Time.time;
	currentTime1 = Time.time;

	controller.SetBool("isFriendly", NPCLike);
	if(executedTime != 0.0f)
	{
		if(currentTime - executedTime > timeToWait)
		{
			executedTime = 0.0f;
			present = false;
			dialog = false;
			displayText = false;
			evidence = false;
		}
	}
	if(executedTime1 != 0.0f)
	{
		if(currentTime1 - executedTime1 > timeToWait1)
		{
			executedTime1 = 0.0f;
			present = false;
			if (dialog) {
				if (randomNum == 3 && npcWords[randomNum] != "empty"){
					Controls.henryMotiveArray[randomNum] = npcWords[randomNum]; 
					likedWordCount++;
				}
				if (randomNum == 4 && npcWords[randomNum] != "empty") {
					Controls.veronicaMotiveArray[randomNum] = npcWords[randomNum];
					likedWordCount++;
				}
				if (randomNum == 5 && npcWords[randomNum] != "empty") {
					Controls.randallMotiveArray[randomNum] = npcWords[randomNum];
					likedWordCount++;
				}
				npcWords[randomNum] = "empty";
				dislikedWordCount++;
				//player.canMove = true;
				randallCam.enabled = false;
				playerCam.enabled = true;
				
				dialog = false;
			}
			displayText = false;
			evidence = false;
			if (cashWad) {
				var temp = Inventory.inventoryItems.IndexOf(Inventory.inventoryItems, "cash");
				Inventory.inventoryItems[temp] = "empty";
				cashWad = false; 
			}
			if (dogPicture) {
				/**int temp = Inventory.inventoryItems.IndexOf("dogPicture");
				Inventory.inventoryItems.Remove("dogPicture");
				Inventory.inventoryItems.Insert(temp, "empty");
				Inventory.emptyCount++;
				dogPicture = false; */
			}
			
		}
	}
	if(Input.GetKey(KeyCode.Escape)) {
		randallCam.enabled = false;
		playerCam.enabled = true;
		present = false;
		dialog = false;
		displayText = false;
		evidence = false;
	}
}

function OnMouseDown() {
	playerCam.enabled = false;
	randallCam.enabled = true;
	//Debug.Log(randallCam.gameObject.name);
	displayText = true;
	//player.canMove = false;
	clickCount++;
	do {
		if (NPCLike) { 
			randomNum = Random.Range (3, 6);
		} else {
			randomNum = Random.Range (0, 3);
		}
} while((npcWords[randomNum].Equals("empty") && likedWordCount < 3 && NPCLike) || (npcWords[randomNum].Equals("empty") && dislikedWordCount < 3));
}

function OnMouseUp() {
	if (clickCount % 2 == 0) {
		displayText = false;
	}
}

function OnGUI() {
	if (displayText && Inventory.playersInvDisplay != null) {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), "Present evidence or ask stuff:");
		if (GUI.Button (new Rect (20,40,220,20), "Present evidence")){
			present = true;
			executedTime = Time.time;
		}
	}
	if (displayText) {
		if (GUI.Button (new Rect (20, 80, 200, 20), "Talk to Randall")) {
			dialog = true;
			executedTime1 = Time.time;
			if (talkCounter < 3)
			{
				Controls.remainingActions = Controls.remainingActions - 1;
				talkCounter= talkCounter+1;
			}
		}
	}
	//If the user has chosen to present evidence, then all of the items in their inventory are displayed
	//a for loop goes through and determines which evidence button has been presented - a flag is set in 
	// a boolean array to determine what evidence to display a response to. 
	if (present) {
		displayText = false;
		for (var i = 0; i < Inventory.count; i++) {
			if (!Inventory.inventoryItems[i].Equals("empty")) {
				if (GUI.Button(new Rect(20, i*20, 200, 20), Inventory.inventoryItems[i])) {
					presentEvidence[i] = true;
					evidence = true;
				}
			}	
		}
	}
		//If the user chooses to talk, then a random dialog option from the NPC's string array is displayed
	if (dialog) {
		displayText = false;
		if (!npcWords[randomNum].Equals("empty")) { 
			executedTime = Time.time;
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), npcWords[randomNum]);
		}
	}
	for (var j = 0; j < Inventory.count; j++) {
		if (presentEvidence[j] == true) {
			presentEvidence[j] = evidence;
			if (evidence && Inventory.inventoryItems[j] == "cash") {
				executedTime1 = Time.time;
				cashWad = true;
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), "Randall snatches the cash wad. Ha-ha!");
			}
			else if (evidence && Inventory.inventoryItems[j] == "DogPicture") { 
				executedTime1 = Time.time;
				dogPicture = true;
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), "Oh, thanks! I lost these!");
				NPCLike = true;
				talkCounter = 0;
			}
			else {
				executedTime1 = Time.time;
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height-200), "You presented " + Inventory.inventoryItems[j] + ". They didn't like it.");
			}
		}

	}
} 
