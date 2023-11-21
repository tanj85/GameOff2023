using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public SpriteRenderer sprite;
    
    void Update()
    {
        // Handle horizontal movement
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        
        // Flip sprite according to movement
        if (rb.velocity.x != 0) { sprite.flipX = rb.velocity.x < 0; }
        if (rb.velocity.x != 0) { sprite.flipY = rb.velocity.y < 0; }
    }
}
