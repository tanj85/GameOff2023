using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCrystal : MonoBehaviour, ICollectible
{
    public int soulCrystals;

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
        GameManager.Instance.currentPortal.AddSoulCrystals(soulCrystals);
        Destroy(gameObject);
    }
}
