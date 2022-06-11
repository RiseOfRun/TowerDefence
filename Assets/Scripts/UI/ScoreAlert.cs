using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreAlert : MonoBehaviour
{
    public float Count;
    public TMP_Text Text;
    public float LifeSize = 1f;
    public float Shift = 20;
    public Vector3 Origin;
    private Camera mainCamera;

    private IEnumerator Start()
    {
        mainCamera = Camera.main;
        Text.text = $"+{Count}$";
        for (float i = 0; i < 1; i += Time.deltaTime / LifeSize)
        {
            transform.position = mainCamera.WorldToScreenPoint(Origin)+ new Vector3(0,Shift*Mathf.Sqrt(i),0);
            yield return null;
        }
        Destroy(gameObject);
    }
}
