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
    public Vector3 position;
    public int TileID;

    public TowerState(Tower t, int tileid)
    {
        Name = t.Pattern.Name;
        TileID = tileid;
        position = t.transform.position;
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
    public float[] PlayerStats = new float[2];
    [FormerlySerializedAs("DObjts")] public List<DObjState> DObjects = new List<DObjState>();
    public TowerState[] Towers = new TowerState[0];


    public LevelState(int currentWave, float[] playerStats, List<DObjState> dObjects, 
        TowerState[] towers)
    {
        CurrentWave = currentWave;
        PlayerStats = playerStats;
        DObjects = dObjects;
        Towers = towers;
    }
}
public class LevelLoader : MonoBehaviour
{
    public bool needToLoad = false;
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

        float[] pStats = {Player.Instance.Lives, Player.Instance.Money};
        LevelState state = new LevelState(level.CurrentWave, pStats,dobjts,TowerStates.ToArray());
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
        Player.Instance.Lives = (int)state.PlayerStats[0];
        Player.Instance.Money = state.PlayerStats[1];
        //Load DObjts
        state.DObjects.ToList().Sort((x, y) => x.TileID.CompareTo(y.TileID));
        int currentDObjID = 0;
        int currentTileID = state.DObjects.Count!=0 ? state.DObjects[currentDObjID].TileID : 0;
        for (int i = 0; i < tiles.Count && currentDObjID < state.DObjects.Count; i++)
        {
            currentTileID = state.DObjects[currentDObjID].TileID;
            DestroyableObject DObj = tiles[i].GetComponentInChildren<DestroyableObject>();
            if (DObj == null)
            {
                continue;
            }
            if (i<currentTileID)
            {
                Destroy(DObj.gameObject);
                continue;
            }
            if (i == currentTileID)
            {
               DObj.Health = state.DObjects[currentDObjID].Health;
               currentDObjID++;
            }
        }
        foreach (var tState in state.Towers)
        {
            var pattern = Towers.Find(x => x.Name == tState.Name);
            pattern.SpawnTower(tiles[tState.TileID]);
            tiles[tState.TileID].OnBuildTower();
        }
        needToLoad = false;
    }

    private void Update()
    {
        if (needToLoad)
        {
            LoadLevel();
        }
    }
}
