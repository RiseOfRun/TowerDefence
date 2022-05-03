using System.Collections;
using System.Collections.Generic;
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
        Vector3 position = indicator.transform.position;
        var bomb = Instantiate(Bomb, position+Shift, Quaternion.identity);
        bomb.Init(new List<Targetable>(), position,Damage,Debuffs);
        return true;
    }
}
