using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectTowerButton : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler, IPointerClickHandler
{
    public TowerPattern Pattern;
    public Image Icon;
    public Text Cost;
    public Text Name;
    
    private Transform defaultParent;
    private int defaultSiblingIndex;
    private Button button;

    void Start()
    {
        defaultParent = transform.parent;
        defaultSiblingIndex = transform.GetSiblingIndex();
        button = GetComponentInChildren<Button>();
    }

    public void Init(TowerPattern Tower)
    {
        Pattern = Tower;
        Name.text = Pattern.Name;
        Cost.text = Pattern.Cost.ToString();
        Icon.sprite = Tower.Icon;
    }

    public void OnButtonClick()
    {
        if (!Pattern.IsUpgrade)
        {
            
            BuildManager.Instance.EnterToBuildMode(Pattern.Mirage, Pattern);
            return;
        }
        
    }

    bool CheckIfOverBuildPanel(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var item in results)
        {
            var panel = item.gameObject.GetComponent<BuildPanel>();
            if (panel!=null)
            {
                return true;
            }
        }

        return false;
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CheckCanDrag())
        {
            OnEndDrag(eventData);
            return;
        }
        transform.parent = defaultParent.parent;
        BuildManager.Instance.EnterToBuildMode(Pattern.Mirage, Pattern);
        Cost.gameObject.SetActive(false);
        Name.gameObject.SetActive(false);

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CheckCanDrag()||!BuildManager.Instance.InBuildMode)
        {
            return;
        }
        transform.position = eventData.position;
        if (Pattern.IsUpgrade)
        {
            return;
        }

        if (CheckIfOverBuildPanel(eventData))
        {
            BuildManager.Instance.Mirage.gameObject.SetActive(false);
            Icon.gameObject.SetActive(true);
        }
        else
        {
            BuildManager.Instance.Mirage.gameObject.SetActive(true);
            Icon.gameObject.SetActive(false);

        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Icon.gameObject.SetActive(true);
        Name.gameObject.SetActive(true);
        Cost.gameObject.SetActive(true);
        transform.SetParent(defaultParent);
        transform.SetSiblingIndex(defaultSiblingIndex);
        if (!CheckCanDrag())
        {
            return;
        }

        if (!CheckIfOverBuildPanel(eventData))
        {
            BuildManager.Instance.BuildTower();
        }

        if (!BuildManager.Instance.InBuildMode && BuildManager.Instance.Mirage!=null)
        {
            BuildManager.Instance.Mirage.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Pattern.IsUpgrade)
        {
            BuildManager.Instance.UpgradeTower(Pattern);
        }
    }

    private bool CheckCanDrag()
    {
        return !Pattern.IsUpgrade && Player.Instance.Money >= Pattern.Cost;
    }
}