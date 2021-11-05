using System;
using UnityEngine.Serialization;

[Serializable]
public class EnemyGroup
{
    [FormerlySerializedAs("TargetableInGroup")] [FormerlySerializedAs("enemyInGroup")] public Enemy EnemyInGroup;
    public int Size;
    public float Health;
    public float Speed;
    public int Score;
    public bool ModifyHealth;
}
