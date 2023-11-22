using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [HideInInspector] public GameObject player;

    #region Collider methods.
    public virtual void OnPlayerTriggerEnter2D(Collider2D collider)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayer)
        {
            Debug.Log("Player entered");
            Entity target = collider.gameObject.GetComponent<Entity>();
            DealDamage(attackDamage, target);
        }
    }

    public virtual void OnPlayerTriggerExit2D(Collider2D collider)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayer)
        {
            Debug.Log("Player exited");
        }
    }
    #endregion

    public virtual void FollowPlayer(){
        state = State.Moving;
        Vector3 targetPosition = player.transform.position;
        Vector3 currentPosition = transform.position;
        Vector3 direction = targetPosition - currentPosition;
        direction.Normalize();
        rb.MovePosition(currentPosition + direction * speed * Time.fixedDeltaTime);
        // Detect if enemy is moving left or right and flip sprite accordingly.
        if (direction.x < 0){
            sprite.flipX = true;
        } else {
            sprite.flipX = false;
        }
        anim.Play("Movement");
    }
}
