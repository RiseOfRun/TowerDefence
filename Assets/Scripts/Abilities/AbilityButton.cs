using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public Image AbilityCoolDown;
    public Ability ConnectedAbility;
    public TMP_Text Name;
    public TMP_Text Cost;
    private float cd;
    private GameObject aPanel;
    // Start is called before the first frame update
    void Start()
    {
        if (ConnectedAbility == null)
        {
            Destroy(gameObject);
            return;
        }
        Name.text = ConnectedAbility.Name;
        Cost.text = ConnectedAbility.Cost.ToString();
        aPanel = transform.parent.gameObject;
        cd = ConnectedAbility.CoolDown;
        ConnectedAbility.State = Ability.AbilityStatement.CoolDown;

    }

    // Update is called once per frame
    void Update()
    {
        if (ConnectedAbility.State == Ability.AbilityStatement.CoolDown)
        {
            cd -= Time.deltaTime;
            if (cd<0)
            {
                cd = 0;
                ConnectedAbility.State = Ability.AbilityStatement.Ready;
            }
            AbilityCoolDown.fillAmount = cd / ConnectedAbility.CoolDown;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CanUse())
        {
            ConnectedAbility.StartAim();
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (ConnectedAbility.State != Ability.AbilityStatement.Aim)
        {
            return;
        }
        ConnectedAbility.Aim();
        
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CanUse())
        {
            ConnectedAbility.EndAim();
            aPanel.SetActive(true);
            bool performed = ConnectedAbility.Perform();
            if (performed)
            {
                cd = ConnectedAbility.CoolDown;
                ConnectedAbility.State = Ability.AbilityStatement.CoolDown;
                Player.Instance.Money -= (int)ConnectedAbility.Cost;
            }
        }
    }

    bool CanUse()
    {
        return (ConnectedAbility.State!=Ability.AbilityStatement.CoolDown &&
                ConnectedAbility.Cost <= Player.Instance.Money);
    }
}
