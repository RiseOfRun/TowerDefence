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
    private bool canDrag = true;
    private Image icon;
    private Transform defaultParent;
    private int defaultSiblingIndex;
    
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
        icon = GetComponent<Image>();
        defaultParent = transform.parent;
        defaultSiblingIndex = transform.GetSiblingIndex();
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
        canDrag = CanUse();
        if (canDrag)
        {
            ConnectedAbility.StartAim();
            transform.parent = defaultParent.parent;
            Name.gameObject.SetActive(false);
            Cost.gameObject.SetActive(false);
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }

        transform.position = eventData.position;
        if (CheckIfOverBuildPanel(eventData))
        {
            icon.enabled = true;
            if (ConnectedAbility.State == Ability.AbilityStatement.Aim)
            {
                ConnectedAbility.EndAim();
            }
            
        }
        else
        {
            icon.enabled = false;
            if (ConnectedAbility.State == Ability.AbilityStatement.Aim)
            {
                ConnectedAbility.Aim();
            }
            else
            {
                ConnectedAbility.StartAim();
                ConnectedAbility.Aim();
            }
        }
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.enabled = true;
        Name.gameObject.SetActive(true);
        Cost.gameObject.SetActive(true);
        transform.SetParent(defaultParent);
        transform.SetSiblingIndex(defaultSiblingIndex);
        if (!canDrag)
        {
            return;
        }
        
        if (CanUse())
        {
            bool performed = ConnectedAbility.Perform();
            if (performed)
            {
                cd = ConnectedAbility.CoolDown;
                ConnectedAbility.State = Ability.AbilityStatement.CoolDown;
                Player.Instance.Money -= (int)ConnectedAbility.Cost;
            }
        }
    }
    bool CheckIfOverBuildPanel(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var item in results)
        {
            if (item.gameObject == aPanel)
            {
                return true;
            }
        }
        return false;
    }
    bool CanUse()
    {
        return (ConnectedAbility.State!=Ability.AbilityStatement.CoolDown &&
                ConnectedAbility.Cost <= Player.Instance.Money);
    }
}
