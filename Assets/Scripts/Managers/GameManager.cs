using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

    public PortalInfo currentPortal = null;

    public Dictionary<int, PortalInfo> portalDict = new Dictionary<int, PortalInfo>();

    public GameObject player;

    public SaveData currSaveData;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // TODO: Replace with functionality for player to decide which save they want to load later on.
        LoadSave(0);
        //portalDict.Add(0, new PortalInfo(0, 1, 1f, 10));
        //portalDict[0].AddSoulCrystals(10);
        //portalDict.Add(1, new PortalInfo(1, 2, 2f, 35));
        //portalDict[1].AddSoulCrystals(99);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LoadWorld(portalDict[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadWorld(portalDict[1]);
        }
    }

    public void GenerateRandomWorld(int level, int dreamEnergy, int? parentID = null, int? childID = null)
    {
        int i = 0;
        while (true)
        {
            if (!portalDict.ContainsKey(i)){
                portalDict.Add(i, new PortalInfo(i, level, Mathf.Pow(10f, level), dreamEnergy));
                if (parentID.HasValue)
                {
                    portalDict[i].SetParentID(parentID.Value);
                }
                if (childID.HasValue)
                {
                    portalDict[i].childrenID.Add(childID.Value);
                }
                break;
            }
            i++;
        }
    }

    // Loads a portal by clearing the current game state to base and then setting up a new world
    public void LoadWorld(PortalInfo portal){
        ClearCurrentWorld();
        SetupWorld(portal);
    }

    /*
     * 1. Set currentPortal as new portal
     * 1.5 Update the world look?
     * 2. force invoke currentPortal resources
     * 3. Instantiate all the interactables on the level
     * 4. Instantiate the Player
     */
    private void SetupWorld(PortalInfo portal)
    {
        currentPortal = portal;

        currentPortal.InvokeResourceChange();
        //update world look?

        // Instantiate interactables
        // Instantiate player
    }


    /*
     * 1. Destroy all active entities (player, monsters, bosses)
     * 2. Destroy all interactables
     */
    private void ClearCurrentWorld(){
        // Destroy active entities
        // Destroy interactables
        currentPortal = null;
    }

    #region Save methods.
    // Saves begin from index 0.
    public void LoadSave(int saveIndex)
    {
        // Delete current in-game dynamic data and replace it with save file data
        currSaveData = null;

        SaveUtil.ReadFile(ref currSaveData, saveIndex); // If successfully able to read in file, currSaveData should no longer be null. If unsuccessful, currSaveData remains null

        // Here is where I load in the save data.
        if(currSaveData != null)
        {
            // Read in the portal dictionary from the save data
            portalDict = currSaveData.portalIDs.Zip(currSaveData.portalInfos, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            // Load in current level
            currentPortal = portalDict[currSaveData.currLevelID];
            LoadWorld(currentPortal);
            currentPortal.InvokeResourceChange(); // May not be needed?

            // Initialize player position from player position in currSaveData
            Vector3 position = currSaveData.playerData.position;
            player.transform.position = position;
        }
        else
        {
            // Load in the scene
            // Setting initial amount of dream energy and soul crystals to 0 because I don't know how much to give player at the beginning
            GenerateRandomWorld(1, 0, 0);
            currentPortal = portalDict[0];
            LoadWorld(currentPortal);
        }
    }

    public void WriteToSave(int saveIndex)
    {
        SaveUtil.WriteFile(ref currSaveData, saveIndex);
    }

    // This function is subject to change and finalization as number of save variables increase!
    public void CreateNewSave(int saveIndex)
    {
        SaveData newSave = new SaveData();

        // deal with portal data
        List<int> portalIDs = new List<int>();
        List<PortalInfo> portalInfos = new List<PortalInfo>();
        foreach (KeyValuePair<int, PortalInfo> portal in portalDict)
        {
            portalIDs.Add(portal.Key);
            portalInfos.Add(portal.Value);
        }
        newSave.portalIDs = portalIDs;
        newSave.portalInfos = portalInfos;
        newSave.currLevelID = currentPortal.id; 

        // deal with player data
        PlayerData playerData = new PlayerData();

        // playerData.inventoryItemDatas = new List<InventoryItemData>(inventory.numInventorySlots); // Use and expand this line of code in the future if we implement an inventory system
        playerData.numSoulCrystals = portalDict[currentPortal.id].soulCrystals;
        playerData.numDreamEnergy = portalDict[currentPortal.id].dreamEnergy;
        playerData.position = player.transform.position;

        newSave.playerData = playerData;

        // write the current save data to the saveIndex save
        SaveUtil.WriteFile(ref newSave, saveIndex);
    }

    // Test function to create a new save file
    [ContextMenu("Create New Save")]
    public void TestCreatingNewSave()
    {
        CreateNewSave(0);
    }
    #endregion

    // Testing updating resource
    [ContextMenu("Update Resource")]
    public void TestUpdateResource()
    {
        currentPortal.InvokeResourceChange();
    }
}
