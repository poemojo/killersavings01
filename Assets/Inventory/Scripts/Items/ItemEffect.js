#pragma strict
//This script allows you to insert code when the Item is used (clicked on in the inventory).

var deleteOnUse = true;
var sayItemWords = true;
var question = false;

private var playersInv : Inventory;
private var item : Item;
public static var count: int = 0;

@script AddComponentMenu ("Inventory/Items/Item Effect")
@script RequireComponent(Item)

//This is where we find the components we need
function Awake ()
{
	playersInv = FindObjectOfType(Inventory); //finding the players inv.
	if (playersInv == null)
	{
		Debug.LogWarning("No 'Inventory' found in game. The Item " + transform.name + " has been disabled for pickup (canGet = false).");
	}
	item = GetComponent(Item);
}

//This is called when the object should be used.

function UseEffect () 
{
	//Debug.LogWarning("geh"); //INSERT CUSTOM CODE HERE!
	
	//Play a sound
	playersInv.gameObject.SendMessage("PlayDropItemSound", SendMessageOptions.DontRequireReceiver);
	
	//This will delete the item on use or remove 1 from the stack (if stackable).
	if (sayItemWords == true)
	{
		ItemWords(); 
		question = true;
		//DeleteUsedItem();
	}
}

function OnGUI () {
	if (question) {
		if (GUI.Button (new Rect (20,40,220,20), "Toss it?")){
				DeleteUsedItem();
				question = false;
			}
		if (GUI.Button (new Rect (20,80,250,20), "Keep it?")){
			question = false;
		}
	}
}



function ItemWords() 
{
	Debug.LogWarning(item.tag);
}

//This takes care of deletion
function DeleteUsedItem()
{
	// For loop goes through the "NPC accessible inventory" and deletes the string that corresponds to the item
	// being deleted. 
	for (var i = 0; i < Inventory.count; i++) {
		if (Inventory.inventoryItems[i] == item.name) {
			Debug.Log("shish");
			Inventory.inventoryItems[i] = null;
		}
	}
	if (item.stack == 1) //Remove item
	{
		playersInv.RemoveItem(this.gameObject.transform);
	}
	else //Remove from stack
	{
		item.stack -= 1;
	}
	Debug.Log(item.name + " has been deleted on use");
}