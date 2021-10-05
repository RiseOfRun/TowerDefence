using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetSystem : MonoBehaviour
{
    [FormerlySerializedAs("EnemyTarget")] public List<Enemy> EnemyTargets = new List<Enemy>();
    public static TargetSystem Instance;
    public TargetMark TargetMarkPrefab;

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
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(r, out hit, float.MaxValue, LayerMask.GetMask("Enemies"))
        || Physics.Raycast(r, out hit, float.MaxValue, LayerMask.GetMask("DestroyableObjs")))
        {
            Debug.Log("HIT");
            Enemy hitEnemy = hit.collider.gameObject.GetComponentInChildren<Enemy>();
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
    }

    // Update is called once per frame
    void Update()
    {
        LMBHandler();
    }
}