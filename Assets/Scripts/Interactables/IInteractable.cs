using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string description { get; }

    bool active { get; set; }
    void Interact();

    // function to toggle hover interactable behavior. For example, if you walk next to a totem, it should
    // somehow denote that it is interactable.
    public void hoverInteract(bool toggle)
    {
        if (toggle)
        {
            Debug.Log("interactable on");
        }
        else
        {
            Debug.Log("interactable off");
        }
    }

    public static IInteractable GrabTargetInteractableOrParentReferenceInteractable(GameObject target)
    {

        IInteractable hitEntity = target.GetComponent<IInteractable>();
        if (hitEntity != null)
        {
            return hitEntity;
        }
        else
        {
            ParentReference parentReference = target.GetComponent<ParentReference>();
            if (parentReference != null && parentReference.parent.GetComponent<IInteractable>() != null)
                return parentReference.parent.GetComponent<IInteractable>();
        }

        return null;
    }
}
