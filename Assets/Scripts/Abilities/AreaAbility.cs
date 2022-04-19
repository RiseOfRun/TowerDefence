using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AreaAbility : Ability
{
    public GameObject IndicatorPrefab;
    [SerializeField] protected float Range;
    protected GameObject indicator;
    public override void StartAim()
    {
        base.StartAim();
        indicator = Instantiate(IndicatorPrefab);
        State = AbiltyStatement.Aim;
        indicator.transform.localScale = new Vector3(Range*2,1,Range*2);
    }

    public override void Aim()
    {
        if (State != AbiltyStatement.Aim) return;
        Plane p = new Plane(Vector3.up, new Vector3(0,0.21f,0));
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        p.Raycast(r, out float enter);
        Vector3 position = r.GetPoint(enter);
        indicator.transform.position = position;
    }
    public override void EndAim()
    {
        base.EndAim();
        Destroy(indicator);
    }
}
