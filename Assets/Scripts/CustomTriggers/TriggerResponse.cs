using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerResponse : MonoBehaviour
{
    public Action<Collider2D> onTriggerEnter2D;
    public Action<Collider2D> onTriggerExit2D;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.transform != gameObject.transform.parent)
        {
            if (onTriggerEnter2D != null)
            {
                onTriggerEnter2D(collider);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.transform != gameObject.transform.parent)
        {
            if (onTriggerExit2D != null)
            {
                onTriggerExit2D(collider);
            }
        }
    }
}
