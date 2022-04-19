using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Ability : ScriptableObject
{
    public enum AbiltyStatement
    {
        Ready,
        Aim,
        CoolDown
    }
    public AbiltyStatement State;
    public float CoolDown;
    public float Cost;

    public virtual void StartAim()
    {
        State = AbiltyStatement.Aim;
    }
    
    public virtual void EndAim()
    {
        State = AbiltyStatement.Ready;
    }
    public virtual bool Perform()
    {
        State = AbiltyStatement.CoolDown;
        return true;
    }

    public virtual void Aim()
    {
    }
}
