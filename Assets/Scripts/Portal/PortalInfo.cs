using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class PortalInfo
{
	public int parentID { get; private set; }
    public List<int> childrenID = new List<int>();
    public int id { get; }
    public int level { get; }
    public float difficultyMultiplier { get; private set; }
    public int dreamEnergy { get; private set; }
    public int soulCrystals { get; private set; }
    public int output { get; private set; }

    public delegate void OnResourceChange(int _dreamEnergy, int _soulCrystals);
    public static event OnResourceChange onResourceChange;

    // private List<Boss> bosses;

    // private List<LevelObjects> AllObjects;

    public PortalInfo(int _id, int _level, float _difficultyMultiplier, int _dreamEnergy){
        id = _id;
        level = _level;
        difficultyMultiplier = _difficultyMultiplier;
        dreamEnergy = _dreamEnergy;
        soulCrystals = 0;
        output = 0;
    }

    public void SetParentID(int _id)
    {
        parentID = _id;
    }

    public void SetOutput(int _output){
        output = _output;
    }

    public void AddSoulCrystals(int amt){
        soulCrystals += amt;
        onResourceChange?.Invoke(dreamEnergy, soulCrystals);
    }

    public void SetDreamEnergy(int _dreamEnergy){
        dreamEnergy = _dreamEnergy;
        onResourceChange?.Invoke(dreamEnergy, soulCrystals);
    }

    public void InvokeResourceChange()
    {
        onResourceChange?.Invoke(dreamEnergy, soulCrystals);
    }
}
