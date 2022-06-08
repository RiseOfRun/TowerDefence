using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.PostProcessing;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/Bomb")]
public class BombAbility : AreaAbility
{
    public AreaDiractedBomb Bomb;
    public float Damage;
    public List<Debuff> Debuffs = new List<Debuff>();
    public Vector3 Shift;

    public override bool Perform()
    {
        bool canPerform = base.Perform();
        if (!canPerform)
        {
            return false;
        }
        Vector3 position = indicator.transform.position;
        EndAim();
        var bomb = Instantiate(Bomb, position+Shift, Quaternion.identity);
        bomb.Init(new List<Targetable>(), position,Damage,Debuffs);
        return true;
    }
}
