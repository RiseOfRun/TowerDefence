using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    
    public Image ForeGround;
    public float ChangeSpeed;
    
    protected float FillPct => CurrentHealth/MaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        var parent = GetComponentInParent<Enemy>();
        MaxHealth = parent.Health;
        CurrentHealth = parent.Health;
        parent.OnHealthChanged += ChangeHealth;
    }

    IEnumerator ChangePct()
    {
        var lastFillPct = ForeGround.fillAmount;
        var to = FillPct;
        for (float i = 0; i < 1; i+=Time.deltaTime/ChangeSpeed)
        {
            ForeGround.fillAmount = Mathf.Lerp(lastFillPct, to,i);
            yield return null;
        }
        ForeGround.fillAmount = to;

    }
    private void ChangeHealth(float current)
    {
        CurrentHealth = current;
        StartCoroutine(ChangePct());
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.Rotate(0,180,0);
    }
}
