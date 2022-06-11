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
        else
        {
            Destroy(gameObject);
        }
    }

    void LMBHandler()
    {
        if (BuildManager.Instance.InBuildMode) return;
        if (!Input.GetMouseButtonDown(0)) return;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(r, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Enemies", "DestroyableObjs","Tower")))
        {
            Debug.Log("HIT");
            Targetable hitEnemy = hit.collider.gameObject.GetComponentInChildren<Targetable>();
            if (hitEnemy==null)
            {
                TargetedTower = hit.collider.gameObject.GetComponentInParent<Tower>();
                BuildManager.Instance.BuildOptionsPanel.OnTowerSelected(TargetedTower);
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
        if (IsPointerOverUIObject())
        {
            return;
        }
        
        if (TargetedTower!=null)
        {
            OnFreeTower?.Invoke();
        }
    }
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
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