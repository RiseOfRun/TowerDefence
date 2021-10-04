using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetSystem : MonoBehaviour
{
    [FormerlySerializedAs("EnemyTarget")] public List<Enemy> EnemyTargets;
    public static TargetSystem Instance;
    public GameObject TargetMarkPrefab;

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
            if (hitEnemy != EnemyTargets)
            {
                EnemyTargets = hitEnemy;
            }
            else
            {
                EnemyTargets = null;
            }
        }
    }

    void TargetMarkHandler()
    {
        if (EnemyTargets == null)
        {
            if (targetMark != null)
            {
                Destroy(targetMark);
            }

            return;
        }

        if (targetMark == null)
        {
            targetMark = Instantiate(TargetMarkPrefab);
        }

        if (targetMark.transform.parent != EnemyTargets.transform)
        {
            targetMark.transform.parent = EnemyTargets.transform;
            targetMark.transform.localPosition = Vector3.zero;
        }

        targetMark.transform.rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        LMBHandler();
        TargetMarkHandler();
    }
}