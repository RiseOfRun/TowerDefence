using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    public static Camera MainCamera;

    private void Start()
    {
        MainCamera = Camera.main;
    }
}
