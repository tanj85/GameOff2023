using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all the info needed in a save file
[Serializable]
public class SaveData
{
    public List<int> portalIDs; // a list of all the portal IDs that the player has visited in a run
    public List<PortalInfo> portalInfos; // a list of all the portals that the player has visited in a run
    public int currLevelID; // index of which level the player last saved 
    public PlayerData playerData; // explained in definition
}

#region PlayerData
// Contains data relating to the player, ex. num dream crystals, inventory, location, etc.
[Serializable]
public class PlayerData
{
    public List<InventoryItemData> inventoryItemDatas; // a list of all the items that the player has in the inventory
    public Vector3 position; // location of the player
}

// Contains the dynamic data of any item that can exist in an inventory. 
// Currently unused but if we implement inventory in the future, we will use this.
[Serializable]
public class InventoryItemData
{
    public string itemName; // name of the item
    public int count; // the amount of this item that the player has
    public int inventorySlotIndex; // the location of this item in the inventory
}
#endregion