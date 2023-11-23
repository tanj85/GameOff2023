using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{
    public string description { get; private set; } = "";

    public bool active { get; set; }

    public int sendWorldID;
    public bool hasSendWorld = false;

    public bool sender = true; // whether portals sends to higher level (true) or lower level (false)
    public int dreamFlowAmt;

    public void Initialize(int _sendWorldID, bool _hasSendWorld, bool _sender, int _dreamFlowAmt)
    {
        sendWorldID = _sendWorldID;
        hasSendWorld = _hasSendWorld;

        sender = _sender;
        dreamFlowAmt = _dreamFlowAmt;
    }
    public void Initialize(PortalInteractableInfo info)
    {
        Initialize(info.sendWorldID, info.hasSendWorld, info.sender, info.dreamFlowAmt);
    }
    public void Interact()
    {
        //Debug.Log(active + "|" + hasSendWorld);
        if (active)
        {
            if (hasSendWorld)
            {
                GameManager.Instance.LoadWorld(GameManager.Instance.portalDict[sendWorldID]);   
            }
            else // the portal should be leading to new frontiers
            {
                if (sender) //leading up a level
                {
                    int portalID = GameManager.Instance.GenerateRandomWorld(GameManager.Instance.currentPortal.level + 1, dreamFlowAmt, 
                        childID: GameManager.Instance.currentPortal.id);
                    hasSendWorld = true;
                    sendWorldID = portalID;
                    GameManager.Instance.currentPortal.parentID = portalID;
                    GameManager.Instance.currentPortal.hasParent = true;
                    GameManager.Instance.LoadWorld(GameManager.Instance.portalDict[sendWorldID]);
                }
                else // leading down a level
                {
                    int portalID = GameManager.Instance.GenerateRandomWorld(GameManager.Instance.currentPortal.level - 1, dreamFlowAmt,
                        parentID: GameManager.Instance.currentPortal.id);
                    hasSendWorld = true;
                    sendWorldID = portalID;
                    GameManager.Instance.currentPortal.childrenID.Add(portalID);
                    GameManager.Instance.LoadWorld(GameManager.Instance.portalDict[sendWorldID]);
                }
            }
        }
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager.onCleanWorld += CleanSelf;
        GameManager.onSyncInteractables += SaveSelf;
        active = true;
    }
    private void OnDisable()
    {
        GameManager.onCleanWorld -= CleanSelf;
        GameManager.onSyncInteractables -= SaveSelf;
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

    public PortalInteractableInfo ConvertToInteractableInfo()
    {
        PortalInteractableInfo info = new PortalInteractableInfo(hasSendWorld, sendWorldID, sender, dreamFlowAmt, transform.position);

        return info;
    }
}
