using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image ForeGround;
    public float ChangeSpeed;
    public bool Visible = false;
    public TMP_Text HealthText;
    private float TargetFillPct => currentHealth/maxHealth;
    private Canvas r;
    private float currentHealth;
    private float maxHealth;
    private Camera mainCamera;


    void Start()
    {
        var parent = GetComponentInParent<Targetable>();
        HealthText.text = ((int) currentHealth).ToString();
        maxHealth = parent.Health;
        currentHealth = parent.Health;
        parent.OnHealthChanged += ChangeHealth;
        r = transform.GetComponentInChildren<Canvas>(true);
        mainCamera = Camera.main;
    }
    private void ChangeHealth(float current)
    {
        currentHealth = current;
        HealthText.text = ((int) currentHealth).ToString();
    }

    void Update()
    {
        float current=ForeGround.fillAmount;
        float target=TargetFillPct;
        if (!Mathf.Approximately(ForeGround.fillAmount,TargetFillPct))
        {
            ForeGround.fillAmount=Mathf.MoveTowards(current, target, Time.deltaTime/ChangeSpeed);
        }
        
        if (Mathf.Approximately(currentHealth,0) || Mathf.Approximately(currentHealth,maxHealth))
        {
            r.enabled = false;
        }
        else
        {
            r.enabled = true;
        }

        if (Math.Abs(currentHealth) < 0.1f)
        {
            Destroy(gameObject);
        }
        transform.rotation = mainCamera.transform.rotation;
    }
}
