using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public TriggerResponse playerTriggerResponse;
    private GameObject player;

    public override void Start()
    {
        base.Start();
        playerTriggerResponse.onTriggerEnter2D = OnPlayerTriggerEnter2D;
        playerTriggerResponse.onTriggerExit2D = OnPlayerTriggerExit2D;
        player = GameObject.FindGameObjectWithTag("Player");

        entityType = EntityType.Enemy;
    }

    public virtual void Update(){
        FollowPlayer();
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
            // TODO: trigger attack animation
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

    public virtual void FollowPlayer(){
        Vector3 targetPosition = player.transform.position;
        Vector3 currentPosition = transform.position;
        Vector3 direction = targetPosition - currentPosition;
        direction.Normalize();
        rb.MovePosition(currentPosition + direction * speed * Time.fixedDeltaTime);
    }
}
