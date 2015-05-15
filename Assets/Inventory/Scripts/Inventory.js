//This is the central piece of the Inventory System.

var Contents : Transform[]; //The content of the Inventory
//An array that the NPC classes can access - made up of Strings
static var inventoryItems: String[];
static var count = 0;
var knifeTake = false;
//static var remainingActions = 0;
var MaxContent : int = 5; //The maximum number of items the Player can carry.
var DebugMode = true; //If this is turned on the Inventory script will output the base of what it's doing to the Console window.

public static var playersInvDisplay : InventoryDisplay; //Keep track of the InventoryDisplay script.

static var itemHolderObject : Transform; //The object the unactive items are going to be parented to. In most cases this is going to be the Inventory object itself.

@script AddComponentMenu ("Inventory/Inventory")

//Handle components and assign the itemHolderObject.
function Awake ()
{
	DontDestroyOnLoad (this);
	inventoryItems = new String[100];
	itemHolderObject = gameObject.transform;
	playersInvDisplay = GetComponent(InventoryDisplay);
	if (playersInvDisplay == null)
	{
		Debug.LogError("No Inventory Display script was found on " + transform.name + " but an Inventory script was.");
		Debug.LogError("Unless a Inventory Display script is added the Inventory won't show. Add it to the same gameobject as the Inventory for maximum performance");
	}
}

//Add an item to the inventory.
function AddItem(Item:Transform)
{
	var newContents = new Array(Contents);
	newContents.Add(Item);
	Contents=newContents.ToBuiltin(Transform); //Array to unity builtin array
	//Item name that is picked up is added to the String array at position count (initialized to 0).
	inventoryItems[count] = Item.name;
	if (DebugMode)
	{
		Debug.Log(inventoryItems[count]+" has been added to inventory");
	}
	count++;
	
	Debug.Log("The inventory count is: " + count);
	//Tell the InventoryDisplay to update the list.
	if (playersInvDisplay != null)
	{
		playersInvDisplay.UpdateInventoryList();
	}
	if (inventoryItems[count] == "Knife" && Knife.knifeTake == true) {
		Controls.remainingActions = Controls.remainingActions-2;
	}
	
}

//Removed an item from the inventory (IT DOESN'T DROP IT).
function RemoveItem(Item:Transform)
{
	var newContents=new Array(Contents);
	var index=0;
	var shouldend=false;
	for(var i:Transform in newContents) //Loop through the Items in the Inventory:
	{
		if(i == Item) //When a match is found, remove the Item.
		{
			newContents.RemoveAt(index);
			shouldend=true;
			//No need to continue running through the loop since we found our item.
		}
		index++;
		
		if(shouldend) //Exit the loop
		{
			Contents=newContents.ToBuiltin(Transform);
			if (DebugMode)
			{
				Debug.Log(Item.name+" has been removed from inventory");
			}
			if (playersInvDisplay != null)
			{
				playersInvDisplay.UpdateInventoryList();
			}
			return;
		}
	}
}

//Dropping an Item from the Inventory
function DropItem(item)
{
	gameObject.SendMessage ("PlayDropItemSound", SendMessageOptions.DontRequireReceiver); //Play sound
	
	var makeDuplicate = false;
	if (item.stack == 1) //Drop item
	{
		RemoveItem(item.transform);
	}
	else //Drop from stack
	{
		item.stack -= 1;
		makeDuplicate = true;
	}
	
	item.DropMeFromThePlayer(makeDuplicate); //Calling the drop function + telling it if the object is stacked or not.
	
	if (DebugMode)
	{
		Debug.Log(item.name + " has been dropped");
	}
}

//This will tell you everything that is in the inventory.
function DebugInfo()
{
		Debug.Log("Inventory Debug - Contents");
	items=0;
	for(var i:Transform in Contents){
		items++;
		if (i.tag == "I am a knife, ooooooh!"){
			this.AddItem(i);
		}
		Debug.Log(i.name);
	}
	Debug.Log("Inventory contains "+items+" Item(s)");
}

//Drawing an 'S' in the scene view on top of the object the Inventory is attached to stay organized.
function OnDrawGizmos ()
{
	Gizmos.DrawIcon (Vector3(transform.position.x, transform.position.y + 2.3, transform.position.z), "InventoryGizmo.png", true);
}