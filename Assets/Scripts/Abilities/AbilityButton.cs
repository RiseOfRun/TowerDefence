using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public Ability ConnectedAbility;

    private float cd;

    private GameObject aPanel;
    // Start is called before the first frame update
    void Start()
    {
        aPanel = transform.parent.gameObject;
        cd = ConnectedAbility.CoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (ConnectedAbility.State == Ability.AbiltyStatement.CoolDown)
        {
            cd -= Time.deltaTime;
            if (cd<0)
            {
                cd = 0;
                ConnectedAbility.State = Ability.AbiltyStatement.Ready;
            }
        }
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CanUse())
        {
            ConnectedAbility.StartAim();
            //aPanel.SetActive(false);
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (ConnectedAbility.State != Ability.AbiltyStatement.Aim)
        {
            return;
        }
        ConnectedAbility.Aim();
        
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        ConnectedAbility.EndAim();
        aPanel.SetActive(true);
        if (CanUse())
        {
            bool performed = ConnectedAbility.Perform();
            if (performed)
            {
                cd = ConnectedAbility.CoolDown;
                Player.Instance.Mana -= ConnectedAbility.Cost;
            }
        }
    }

    bool CanUse()
    {
        return (ConnectedAbility.State == Ability.AbiltyStatement.Ready &&
                ConnectedAbility.Cost <= Player.Instance.Mana);
    }
}
