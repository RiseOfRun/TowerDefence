using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;


public class Colorizable : MonoBehaviour
{
    [Range(0,1)]public float lerp;
    public int[] NumbersOfElementsToColorize;
    public Color DefaultColor;
    private Material[] objectMaterials;
    private Material[] originMaterial;
    void Start()
    {
        var mesh = GetComponentInChildren<MeshRenderer>();
        originMaterial = mesh.sharedMaterials;
        objectMaterials = mesh.materials;
        lerp = (float)LevelController.Instance.CurrentWave / LevelController.Instance.WaveCount;
        SetColor(DefaultColor,lerp);
    }

    [ContextMenu("SetColor")]
    public void SetColor(Color edition, float lerp)
    {
        foreach (var elem in NumbersOfElementsToColorize)
        {
            objectMaterials[elem].color = Color.Lerp(originMaterial[elem].color, edition, lerp / 2);
        }
    }

    private void Update()
    {
        //SetColor(DefaultColor,lerp);
    }
}
