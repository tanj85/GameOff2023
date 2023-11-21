using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public TriggerResponse playerTriggerResponse;

    public override void Start()
    {
        base.Start();
        playerTriggerResponse.onTriggerEnter2D = OnPlayerTriggerEnter2D;
        playerTriggerResponse.onTriggerExit2D = OnPlayerTriggerExit2D;

        entityType = EntityType.Enemy;
    }

    #region Collider methods.
    private void OnPlayerTriggerEnter2D(Collider2D collider)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayer)
        {
            Debug.Log("Player entered");
            Entity target = collider.gameObject.GetComponent<Entity>();
            DealDamage(attackDamage, target);
        }
    }

    private void OnPlayerTriggerExit2D(Collider2D collider)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayer)
        {
            Debug.Log("Player exited");
        }
    }
    #endregion
}
