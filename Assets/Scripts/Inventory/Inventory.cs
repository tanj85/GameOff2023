using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int numInventorySlots;
    [SerializeField] public static Weapon persistentWeapon;
    [SerializeField] public static List<ICollectible> inventory;

    void Start(){
        inventory = new List<ICollectible>(numInventorySlots);
    }

    public static void AddItem(ICollectible item){
        if (inventory.Count < inventory.Capacity){
            inventory.Add(item);
        } else {
            Debug.Log("Inventory full");
        }
    }
}
