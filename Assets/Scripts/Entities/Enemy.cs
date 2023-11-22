using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [HideInInspector] public GameObject player;
    [SerializeField]float timer = 0f;

    #region Collider methods.
    public virtual void OnPlayerTriggerEnter2D(Collider2D collider)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayer)
        {
            //Debug.Log("Player entered");
        }
    }

    public virtual void OnPlayerTriggerStay2D(Collider2D collider)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayer)
        {
            Entity target = collider.gameObject.GetComponent<Entity>();
            timer += Time.deltaTime;
            if (timer >= attackCooldown){
                DealDamage(attackDamage, target);
                timer = 0f;
            }
        }
    }

    public virtual void OnPlayerTriggerExit2D(Collider2D collider)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == playerLayer)
        {
            //Debug.Log("Player exited");
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
        if (anim != null) anim.Play("Movement");
    }
}
