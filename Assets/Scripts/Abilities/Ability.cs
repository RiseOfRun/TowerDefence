using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Ability : ScriptableObject
{
    public enum AbilityStatement
    {
        Ready,
        Aim,
        CoolDown
    }
    public AbilityStatement State;
    public float CoolDown;
    [Header("Parameters")]
    public float Cost;
    public string Name;
    public Sprite Icon;

    public virtual void StartAim()
    {
        State = AbilityStatement.Aim;
    }
    
    public virtual void EndAim()
    {
        State = AbilityStatement.Ready;
    }
    public virtual bool Perform()
    {
        State = AbilityStatement.CoolDown;
        return true;
    }

    public virtual void Aim()
    {
    }
}
