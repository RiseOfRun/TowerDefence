using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public Image ForeGround;
    public float ChangeSpeed;
    public bool Visible = false;
    public TMP_Text HealthText;
    private float TargetFillPct => CurrentHealth/MaxHealth;
    private Canvas r;
    // Start is called before the first frame update
    void Start()
    {
        var parent = GetComponentInParent<Enemy>();
        HealthText.text = ((int) CurrentHealth).ToString();
        MaxHealth = parent.Health;
        CurrentHealth = parent.Health;
        parent.OnHealthChanged += ChangeHealth;
        r = transform.GetComponentInChildren<Canvas>();
    }

    IEnumerator ChangePct()
    {
        var lastFillPct = ForeGround.fillAmount;
        var to = TargetFillPct;
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
        HealthText.text = ((int) CurrentHealth).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float current=ForeGround.fillAmount;
        float target=TargetFillPct;
        if (!Mathf.Approximately(ForeGround.fillAmount,TargetFillPct))
        {
            ForeGround.fillAmount=Mathf.MoveTowards(current, target, Time.deltaTime/ChangeSpeed);
        }
        
        if (Mathf.Approximately(CurrentHealth,0) || Mathf.Approximately(CurrentHealth,MaxHealth))
        {
            r.enabled = false;
            //transform.Rotate(0,180,0);
        }
        else
        {
            r.enabled = true;
        }
        transform.rotation = Camera.main.transform.rotation;
    }
}
