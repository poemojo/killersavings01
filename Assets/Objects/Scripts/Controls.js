#pragma strict

public class Controls extends MonoBehaviour
{
	var Henry: GameObject;
	var Randall: GameObject;
	var Veronica: GameObject;
	var Player: GameObject;
	public static var levels: String[] = new String[18];
	public static var inventoryArray: String[] = new String[18];
	public static var henryMotiveArray: String[] = new String[6];
	public static var randallMotiveArray: String[] = new String[6];
	public static var veronicaMotiveArray: String[] = new String[6];
	public static var listCount: int = 0;
	public static var emptyCount: int = 0;
	public static var maxActions: int = 0;
	public static var remainingActions: int = 0;
	public static var levelCount: int = 0;
	var print1: boolean = false;
	var endGame: boolean = false;
	var throwAway: boolean = false;
	var displayNPCs: boolean = true;
	var displayMotives: boolean = false;
	var displayMotives2: boolean = false;
	var displayMotives3: boolean = false;
	var displayEvidence: boolean = false;
	var displayEvidence2: boolean = false;
	var displayEvidence3: boolean = false;;
	var correctMurderWeapon: int = 0;	//0 is initial; 1 is true; 2 is false
	var correctMotive: int = 0;			//0 is initial; 1 is true; 2 is false
	var correctNPC: boolean = false;
	private var currentTime: float = 0.0f;
	private var executedTime: float = 0.0f;
	private var timeToWait: float = 2.0f;
	public var henryCam: Camera;
	public var randallCam: Camera;
	public var veronicaCam: Camera;

	function Start () {
		
	}
	
	function Update ()
	{
	
		if (levelCount > 5) {
			endGame = true;
		}
		if (levelCount % 2 != 0) { 
			maxActions = 2;
			if (remainingActions <= 0) {
				levelCount++;
				remainingActions = 6;
				Henry.transform.position = Vector3(30,0, 140);
				henryCam.transform.position = Vector3(29, 8, 137);
				Randall.transform.position = Vector3(65,0,175);
				randallCam.transform.position = Vector3(68, 6, 180);
				Veronica.transform.position = Vector3(100,0,20);
				veronicaCam.transform.position = Vector3(99, 6, 23);
				Player.transform.position = Vector3(20,10,80);
			}
		} 
		else {
			maxActions = 6;
			if (remainingActions <= 0) {
				levelCount++;
				remainingActions = 2;
				Henry.transform.position = Vector3(30,0, 100);
				henryCam.transform.position = Vector3(29, 8, 97);
				Randall.transform.position = Vector3(65,0,15);
				randallCam.transform.position = Vector3(67, 6, 20);
				Veronica.transform.position = Vector3(170,0,10);
				veronicaCam.transform.position = Vector3(170, 7.5, 13);
				Player.transform.position = Vector3(80,10,10);
			}
		}
	}

	function OnGUI() {
		GUI.Box (new Rect(Screen.width - 200,Screen.height - 50,170,25), "Action points remaining: " + remainingActions.ToString());

		if (endGame) {
			print1 = false;
			/** If the user picks Henry as the killer*/
			if (displayNPCs) { 
				if (GUI.Button (new Rect (100, 280, 100, 20), "Henry") && displayNPCs) {
					displayMotives = true;
					correctNPC = true;
					displayNPCs = false;
				}
				if (GUI.Button (new Rect (200, 280, 100, 20), "Veronica")) {
					displayMotives2 = true;
					correctNPC = false;
					displayNPCs = false;
				}
				if (GUI.Button (new Rect (300, 280, 100, 20), "Randall")) {
					displayMotives3 = true;
					correctNPC = false;
					displayNPCs = false;
				}
			}
			if (displayMotives) {
				print (henryMotiveArray.Length);
				for (var i = 0; i < henryMotiveArray.Length; i++) {
					if (GUI.Button (new Rect (200, (50*i), 500, 20), henryMotiveArray [i]) && displayMotives) {
						if (henryMotiveArray[i] == "I did it...what, you think I'm kidding?") {
							displayEvidence = true;
							correctMotive = 1;
							displayMotives = false;
						} 
						else {
							displayEvidence = true;
							correctMotive = 2;
							displayMotives = false;
						}
					} 
				}
			}
			if (displayEvidence) {
				for (var j = 0; j < Inventory.count; j++) {
					if (inventoryArray[j].Equals ("empty") == false) {
						if (GUI.Button (new Rect ((100 * j), 280, 100, 20), Inventory.inventoryItems[j]) && displayEvidence) {
							if (Inventory.inventoryItems[j] == "knife") {
								correctMurderWeapon = 1;
								displayEvidence = false;
							}
							else {
								correctMurderWeapon = 2;
								displayEvidence = false;
							}
						}
					}
				}
			}

			if (displayMotives2) {
				print (veronicaMotiveArray.Length);
				for (var k = 0; k < veronicaMotiveArray.Length; k++) {
					if (GUI.Button (new Rect (200, (50*k), 500, 20), veronicaMotiveArray [k]) && displayMotives2) {
						if (veronicaMotiveArray[k] == "I may or may not have an STD...") {
							displayEvidence2 = true;
							correctMotive = 2;
							displayMotives2 = false;
						} 
						else {
							displayEvidence2 = true;
							correctMotive = 2;
							displayMotives2 = false;
						}
					} 
				}
			}
			if (displayEvidence2) {
				for (var l = 0; l < inventoryArray.length; l++) {
					if (Inventory.inventoryItems[l].Equals ("empty") == false) {
						if (GUI.Button (new Rect ((100 * l), 280, 100, 20), Inventory.inventoryItems[l]) && displayEvidence2) {
							if (inventoryArray [l] == "knife") {
								correctMurderWeapon = 2;
								displayEvidence2 = false;
							}
							else {
								correctMurderWeapon = 2;
								displayEvidence2 = false;
							}
						}
					}
				}
			}
			if (displayMotives3) {
				print (randallMotiveArray.Length);
				for (var m = 0; m < randallMotiveArray.Length; m++) {
					if (GUI.Button (new Rect (200, (50*m), 500, 20), randallMotiveArray [m]) && displayMotives3) {
						if (randallMotiveArray[m] == "I hate the manager.") {
							displayEvidence3 = true;
							correctMotive = 2;
							displayMotives3 = false;
						} 
						else {
							displayEvidence3 = true;
							correctMotive = 2;
							displayMotives3 = false;
						}
					} 
				}
			}
			if (displayEvidence3) {
				for (var n = 0; n < inventoryArray.length; n++) {
					if (Inventory.inventoryItems[n].Equals ("empty") == false) {
						if (GUI.Button(new Rect ((100 * n), 280, 100, 20), Inventory.inventoryItems[n]) && displayEvidence3) {
							if (inventoryArray [n] == "knife") {
								correctMurderWeapon = 2;
								displayEvidence3 = false;
							}
							else {
								correctMurderWeapon = 2;
								displayEvidence3 = false;
							}
						}
					}
				}
			}

			if (correctMurderWeapon == 1 && correctNPC && correctMotive == 1) {
				GUI.Box (new Rect (0, 0, Screen.width, Screen.height - 200), "You won! Go outside pls");
			}
			else if (correctMurderWeapon == 2 && correctMotive == 2 && !correctNPC){
				GUI.Box (new Rect (0, 0, Screen.width, Screen.height - 200), "You lost! Try again or die");
			}

		}
	} 
}