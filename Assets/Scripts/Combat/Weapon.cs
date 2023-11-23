using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, ICollectible
{
    public string weaponType;
    //public WeaponType weaponType;
    public int attackDamage;
    public int attackCooldown;

    void OnTriggerEnter2D(Collider2D collider)
    {
        int playerLayerMask = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayerMask)
        {
            Collect();
        }
    }

    public void Collect()
    {
        Inventory.AddItem(this);
        Destroy(gameObject);
    }
}