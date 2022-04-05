using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TargetSystem : MonoBehaviour
{
    [FormerlySerializedAs("EnemyTarget")] public List<Targetable> EnemyTargets = new List<Targetable>();
    public Tower TargetedTower;
    public static TargetSystem Instance;
    public TargetMark TargetMarkPrefab;
    public Action OnFreeTower;
    private GameObject targetMark;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    void LMBHandler()
    {
        if (BuildManager.Instance.InBuildMode) return;
        if (!Input.GetMouseButtonDown(0)) return;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(r, out hit, float.MaxValue, LayerMask.GetMask("Enemies", "DestroyableObjs")))
        {
            Debug.Log("HIT");
            Targetable hitEnemy = hit.collider.gameObject.GetComponentInChildren<Targetable>();
            if (hitEnemy==null)
            {
                return;
            }
            if (!EnemyTargets.Contains(hitEnemy))
            {
                EnemyTargets.Add(hitEnemy);
                Instantiate(TargetMarkPrefab, hitEnemy.transform);
            }
            else
            {
                EnemyTargets.Remove(hitEnemy);
                Destroy(hitEnemy.GetComponentInChildren<TargetMark>().gameObject);
            }
        }

        if (Physics.Raycast(r, out hit, float.MaxValue, LayerMask.GetMask("Tower")))
        {
            TargetedTower = hit.collider.gameObject.GetComponentInParent<Tower>();
            BuildManager.Instance.BuildPanel.OnTowerSelected(TargetedTower);
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (TargetedTower!=null)
        {
            OnFreeTower?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BuildManager.Instance.InBuildMode)
        {
            return;
        }

        LMBHandler();
    }
}