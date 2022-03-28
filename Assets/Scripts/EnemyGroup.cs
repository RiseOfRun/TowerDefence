using System;
using UnityEngine.Serialization;

[Serializable]
public class EnemyGroup
{
    [FormerlySerializedAs("TargetableInGroup")] [FormerlySerializedAs("enemyInGroup")] public Enemy EnemyInGroup;
    public int Size;
    public float BaseHealth;
    public float BaseSpeed;
    public int BaseScore;
}
