using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public bool Available = true;
    public Material AvailableMaterial;
    public Material InvalidMaterial;
    private Vector3 defaultPosition;
    private MeshRenderer mesh;
    private bool isActive = false;

    void Start()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        defaultPosition = transform.position;
    }

    public void SetMaterial(bool available)
    {
        mesh.material = available ? AvailableMaterial : InvalidMaterial;
    }

    void Update()
    {
        CheckTransform();
        CheckColor();
    }

    void CheckTransform()
    {
        isActive = true;
        if (BuildManager.Instance.InBuildMode)
        {
            if (!BuildManager.Instance.Mirage.isActiveAndEnabled)
            {
                transform.position = defaultPosition;
                isActive = false;
                return;
            }

            transform.position = BuildManager.Instance.Mirage.transform.position;
            float range = BuildManager.Instance.currentTower.Range * 2;
            transform.localScale = new Vector3(range, 1, range);
            return;
        }

        if (TargetSystem.Instance.TargetedTower != null)
        {
            transform.position = TargetSystem.Instance.TargetedTower.transform.position;
            float range = TargetSystem.Instance.TargetedTower.Pattern.Range * 2;
            transform.localScale = new Vector3(range, 1, range);
            return;
        }

        isActive = false;
        transform.position = defaultPosition;
    }

    void CheckColor()
    {
        if (!BuildManager.Instance.InBuildMode)
        {
            SetMaterial(true);
            return;
        }
        
        Ray r = new Ray(transform.position+new Vector3(0,1,0), Vector3.down);
        if (Physics.Raycast(r, out RaycastHit hitInfo, float.MaxValue, LayerMask.GetMask("Ground")))
        {
            Square sq = hitInfo.collider.gameObject.GetComponentInParent<Square>();
         
            if (sq !=null)
            {
                SetMaterial(sq.CanBuild);
            }
        }
        else
        {
            SetMaterial(false);
        }
    }
}