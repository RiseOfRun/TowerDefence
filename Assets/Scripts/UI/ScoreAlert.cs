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
    private IEnumerator Start()
    {
        Text.text = $"+{Count}$";
        for (float i = 0; i < 1; i += Time.deltaTime / LifeSize)
        {
            transform.position = Camera.main.WorldToScreenPoint(Origin)+ new Vector3(0,Shift*Mathf.Sqrt(i),0);
            yield return null;
        }
        Destroy(gameObject);
    }
}
