using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Sprite spriteFaceRight;
    public Sprite spriteFaceDown;
    //public Animator anim;
    Vector2 movement;

    private IInteractable currentInteractable;
    private bool nearInteractable = false;
    public Weapon weaponInHand;

    public override void Start()
    {
        base.Start();

        entityType = EntityType.Player;
    }

    public override void Update()
    {
        // Handle movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        //if (movement.x == 0 && movement.y == 0) anim.Play("Idle");
        if (movement.x != 0){
            //anim.Play("MoveHorizontal");
        } else if (movement.y != 0){
            //anim.Play("MoveVertical");
        }
        if (movement.x < 0) {sprite.flipX = true;} else {sprite.flipX = false; }

        if (nearInteractable && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable?.Interact();
        }
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = IInteractable.GrabTargetInteractableOrParentReferenceInteractable(collision.gameObject);
        if (interactable != null)
        {
            currentInteractable = interactable;
            nearInteractable = true;
            currentInteractable.hoverInteract(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = IInteractable.GrabTargetInteractableOrParentReferenceInteractable(collision.gameObject);
        if (interactable != null)
        {
            currentInteractable?.hoverInteract(false);
            if (currentInteractable == interactable)
            {
                nearInteractable = false;
                currentInteractable = null;
            }
        }
    }

    public override void Die()
    {
        base.Die();
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (weaponInHand != null)
        {
            Destroy(weaponInHand.gameObject);
        }
        weaponInHand = weapon;
    }

    [ContextMenu("Test Equip Weapon")]
    public void TestEquipWeapon()
    {
        Weapon firstWeapon = (Weapon)Inventory.inventory[0];
        Debug.Log($"Weapon Type: {firstWeapon.weaponType}, Attack Damage: {firstWeapon.attackDamage}, Attack Cooldown: {firstWeapon.attackCooldown}");
        EquipWeapon(firstWeapon);
    }
}