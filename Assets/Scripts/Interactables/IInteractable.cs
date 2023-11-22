using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string description { get; }

    bool active { get; set; }
    void Interact();
}
