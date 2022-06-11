using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
class TowerState
{
    public string Name;
    public Vector3 Position;
    public int TileID;

    public TowerState(Tower t, int tileid)
    {
        Name = t.Pattern.Name;
        TileID = tileid;
        Position = t.transform.position;
    }
}
[Serializable]
class DObjState
{
    public float Health;
    public int TileID;

    public DObjState(float health, int tileID)
    {
        Health = health;
        TileID = tileID;
    }
}
[Serializable]
class LevelState
{
    public int CurrentWave;
    public int PlayerHealth;
    public float PlayerMoney;
    public List<DObjState> DObjects = new List<DObjState>();
    public TowerState[] Towers = new TowerState[0];


    public LevelState(int currentWave, int playerHealth,float playerMoney , List<DObjState> dObjects, 
        TowerState[] towers)
    {
        CurrentWave = currentWave;
        PlayerHealth = playerHealth;
        PlayerMoney = playerMoney;
        DObjects = dObjects;
        Towers = towers;
    }
}
public class LevelLoader : MonoBehaviour
{
    public bool NeedToLoad = false;
    public LevelController level;
    private List<Square> tiles = new List<Square>();
    private List<TowerPattern> Towers = new List<TowerPattern>();
    
    public void SetLayout(LevelController l)
    {
        level = l;
        foreach (var t in level.TowersToBuild)
        {
            Towers.Add(t);
            foreach (var tup in t.Upgrades)
            {
                Towers.Add(tup);
            }
        }
        level.EndWave.AddListener(SaveLevel);
        foreach (var tile in level.GetComponentsInChildren<Square>())
        {
            tiles.Add(tile);
        }
    }
    public void SaveLevel()
    {

        PlayerPrefs.SetInt("OnLevel", GameManager.CurrentLevel);
        List<DObjState> dobjts = new List<DObjState>();
        List<TowerState> TowerStates = new List<TowerState>();
        
        foreach (var t in level.Towers.Where(x =>x!=null))
        {
            int tileID = tiles.IndexOf(t.GetComponentInParent<Square>());
            TowerStates.Add(new TowerState(t,tileID));
        }
        foreach (var dobj in level.GetComponentsInChildren<DestroyableObject>())
        {
            int tileID = tiles.IndexOf(dobj.GetComponentInParent<Square>());
            dobjts.Add(new DObjState(dobj.Health, tileID));
        }
        LevelState state = new LevelState(level.CurrentWave, Player.Instance.Lives,Player.Instance.Money,
            dobjts,TowerStates.ToArray());
        string json = JsonUtility.ToJson(state);
        LevelState tmpCheckState = JsonUtility.FromJson<LevelState>(json);
        PlayerPrefs.SetString("LevelState", json);
    }

    public void LoadLevel()
    {
        string json = PlayerPrefs.GetString("LevelState");
        LevelState state = JsonUtility.FromJson<LevelState>(json);
        level.CurrentWave = state.CurrentWave;
        //Load PlayerState
        Player.Instance.Lives = state.PlayerHealth;
        Player.Instance.Money = state.PlayerMoney;
        //Load DObjts
        state.DObjects.ToList().Sort((x, y) => x.TileID.CompareTo(y.TileID));
        int currentDObjID = 0;
        for (int i = 0; i < tiles.Count && currentDObjID < state.DObjects.Count; i++)
        {
            int currentTileID = state.DObjects[currentDObjID].TileID;
            DestroyableObject dObj = tiles[i].GetComponentInChildren<DestroyableObject>();
            if (dObj == null)
            {
                continue;
            }
            if (i<currentTileID)
            {
                Destroy(dObj.gameObject);
                continue;
            }
            if (i == currentTileID)
            {
               dObj.Health = state.DObjects[currentDObjID].Health;
               currentDObjID++;
            }
        }
        foreach (var tState in state.Towers)
        {
            var pattern = Towers.Find(x => x.Name == tState.Name);
            pattern.SpawnTower(tiles[tState.TileID]);
            tiles[tState.TileID].OnBuildTower();
        }
        NeedToLoad = false;
    }

    private void Update()
    {
        if (NeedToLoad)
        {
            LoadLevel();
        }
    }
}
