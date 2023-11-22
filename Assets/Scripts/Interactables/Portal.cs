using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{
    public string description { get; private set; } = "";

    public bool active { get; set; }

    public int sendWorldID;
    public bool hasSendWorld = false;

    public void Initialize(int _sendWorldID, bool _hasSendWorld)
    {
        sendWorldID = _sendWorldID;
        hasSendWorld = _hasSendWorld;
    }
    public void Interact()
    {
        //Debug.Log(active + "|" + hasSendWorld);
        if (active && hasSendWorld)
        {
            //Debug.Log("trying");
            GameManager.Instance.LoadWorld(GameManager.Instance.portalDict[sendWorldID]);
        }
    }

    public PortalInteractableInfo ConvertToInteractableInfo()
    {
        PortalInteractableInfo info = new PortalInteractableInfo();

        info.location = transform.position;
        info.interactableType = InteractableInfo.InteractableType.Portal;
        info.sendWorldID = sendWorldID;
        info.hasSendWorld = hasSendWorld;

        return info;
    }

    private void OnEnable()
    {
        GameManager.onCleanWorld += CleanSelf;
        GameManager.onSave += SaveSelf;
        active = true;
    }
    private void OnDisable()
    {
        GameManager.onCleanWorld -= CleanSelf;
        GameManager.onSave -= SaveSelf;
    }

    public void CleanSelf()
    {
        GameManager.Instance.currentPortal.portalInteractableInfos.Add(ConvertToInteractableInfo());

        Destroy(gameObject);
    }

    public void SaveSelf()
    {
        GameManager.Instance.currentPortal.portalInteractableInfos.Add(ConvertToInteractableInfo());
    }
}
