using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;

public class ManualDeltaTimer
{
    public float CallDown;
    public Action OnTick;
    
    public float LeastTime = 0;
    public int Capacity = 1;
    public bool Stop = false;
    public void Next(float count)
    {
        if (Stop)
        {
            return;
        }
        if (LeastTime<=0)
        {
            LeastTime = 0;
            return;
        }
        LeastTime -= count;
    }
    public void TryToDoAction()
    {
        if (Stop)
        {
            return;
        }
        if (LeastTime>0)
        {
            return;
        }

        Capacity = (int) (Mathf.Floor(Mathf.Abs(LeastTime) / CallDown) + 1);
        
        for (int i = 0; i < Capacity; i++)
        {
            OnTick();
        }

        Capacity = 0;
        LeastTime = CallDown;
    }
}
