using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    private Vector3 defaultPosition;
    void Start()
    {
        defaultPosition = transform.position;
    }
    void Update()
    {
        CheckTransform();
    }

    void CheckTransform()
    {
        if (BuildManager.Instance.InBuildMode)
        {
            if (!BuildManager.Instance.Mirage.isActiveAndEnabled)
            {
                transform.position = defaultPosition;
                return;
            }
            transform.position = BuildManager.Instance.Mirage.transform.position;
            float range = BuildManager.Instance.currentTower.Range*2;
            transform.localScale = new Vector3(range,1,range);
            return;
        }
        else if(TargetSystem.Instance.TargetedTower!=null)
        {
            {
                transform.position = TargetSystem.Instance.TargetedTower.transform.position;
                float range = TargetSystem.Instance.TargetedTower.Pattern.Range*2;
                transform.localScale = new Vector3(range,1,range);
                return;
            }
        }

        transform.position = defaultPosition;
    }
}
