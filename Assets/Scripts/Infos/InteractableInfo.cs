using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractableInfo
{
    public Vector2 location;

    public enum InteractableType
    {
        BossTotem,
        Portal
    }

    static Dictionary<InteractableType, System.Type> interactableDict = new Dictionary<InteractableType, System.Type>()
    {
        [InteractableType.BossTotem] = typeof(BossTotemInfo),
        [InteractableType.Portal] = typeof(PortalInteractableInfo)
    };

    public InteractableType interactableType;
}