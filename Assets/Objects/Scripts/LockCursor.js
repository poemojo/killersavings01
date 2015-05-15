#pragma strict

var toggle: boolean = false;
var henry: Henry;
var randall: Randall;
var veronica: Veronica;
var scissors: Scissors;
var knife: Knife;
var earbuds: Earbuds;
var legoflamb: LegOfLamb;
var carrots: Carrots; 
//var veronica: Veronica;

var player: GameObject;
function Start () {
	Screen.lockCursor = true;
}

function Update () {
	OnMouseDown();
}

function OnMouseDown()
{
	Screen.lockCursor = false;
	if (randall.displayText || randall.present || henry.displayText || henry.present || veronica.displayText || veronica.present || scissors.displayText || knife.displayText || earbuds.displayText || legoflamb.displayText || carrots.displayText || InventoryDisplay.displayInventory || Knife.yesButtonClick && this.contains(Inventory.inventoryItems, "Knife", Inventory.count) == -1)
	{
		Screen.lockCursor = false;
		//veronica.displayText
	}
	else
		Screen.lockCursor = true;

}

function OnGUI () {
	if (Screen.lockCursor) {
	    var centeredStyle = GUI.skin.GetStyle("Label");
	    centeredStyle.alignment = TextAnchor.UpperCenter;
	    GUI.Label (Rect (Screen.width/2-50, Screen.height/2-25, 100, 50), "+", centeredStyle);
	}
}

function contains(a: String[], b: String, count: int) {
	for (var i = 0; i < count; i++) {
		if (a[i] == b) {
			return 1;
		}
	}
	return -1;
}



