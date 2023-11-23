using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PortalInteractableInfo : InteractableInfo
{
    public bool hasSendWorld;
    public int sendWorldID;

    public bool sender = true; // whether portals sends to higher level (true) or lower level (false)
    public int dreamFlowAmt;

    public PortalInteractableInfo(bool _hasSendWorld, int _sendWorldID, bool _sender, int _dreamFlowAmt, Vector2 _location)
    {
        interactableType = InteractableType.Portal;
        hasSendWorld = _hasSendWorld;
        sendWorldID = _sendWorldID;
        sender = _sender;
        dreamFlowAmt = _dreamFlowAmt;
        location = _location;
    }
}
