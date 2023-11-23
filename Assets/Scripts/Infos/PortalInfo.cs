using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

[System.Serializable]
public class PortalInfo
{
    public bool hasParent = false;
	public int parentID;
    public List<int> childrenID = new List<int>();
    public int id;
    public int level;
    public float difficultyMultiplier;
    public int dreamEnergy;
    public int totalDreamEnergyAcquired;
    public int soulCrystals;
    public int output;

    public delegate void OnResourceChange(int _dreamEnergy, int _soulCrystals);
    public static event OnResourceChange onResourceChange;

    public List<BossTotemInfo> bossTotemInfos = new List<BossTotemInfo>();
    public List<PortalInteractableInfo> portalInteractableInfos = new List<PortalInteractableInfo>();

    public PortalInfo(int _id, int _level, float _difficultyMultiplier){
        id = _id;
        level = _level;
        difficultyMultiplier = _difficultyMultiplier;
        dreamEnergy = 0;
        totalDreamEnergyAcquired = 0;
        soulCrystals = 0;
        output = 0;
    }

    public void SetParentID(int _id)
    {
        parentID = _id;
        hasParent = true;
    }

    public void SetOutput(int _output){
        output = _output;
    }

    public void AddSoulCrystals(int amt){
        soulCrystals += amt;
        onResourceChange?.Invoke(dreamEnergy, soulCrystals);
    }

    public void AddDreamEnergy(int _dreamEnergy)
    {
        totalDreamEnergyAcquired += _dreamEnergy;
        dreamEnergy += _dreamEnergy;
        onResourceChange?.Invoke(dreamEnergy, soulCrystals);
    }

    public void SubtractDreamEnergy(int _dreamEnergy)
    {
        dreamEnergy -= _dreamEnergy;
        onResourceChange?.Invoke(dreamEnergy, soulCrystals);
    }

    public void SyncSenderDreamFlowAmts(int newAmt)
    {
        List<PortalInteractableInfo> senderPortals = portalInteractableInfos.Where(x => x.sender).ToList();

        foreach (PortalInteractableInfo info in senderPortals)
        {
            info.dreamFlowAmt = newAmt;
        }
    }

    public void ClearAllInteractableLists()
    {
        bossTotemInfos = new List<BossTotemInfo>();
        portalInteractableInfos = new List<PortalInteractableInfo>();
    }

    public void InvokeResourceChange()
    {
        onResourceChange?.Invoke(dreamEnergy, soulCrystals);
    }
}
