using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectTowerButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public TowerPattern Pattern;
    public Image Icon;
    public TMP_Text Cost;
    public TMP_Text Name;

    private Transform defaultParent;
    private int defaultSiblingIndex;
    private Button button;

    void Start()
    {
        defaultParent = transform.parent;
        defaultSiblingIndex = transform.GetSiblingIndex();
        button = GetComponentInChildren<Button>();
    }

    public void Init(TowerPattern tower)
    {
        Pattern = tower;
        Name.text = Pattern.Name;
        Cost.text = Pattern.Cost.ToString();
        Icon.sprite = tower.Icon;
    }
    

    bool CheckIfOverBuildPanel(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var item in results)
        {
            var panel = item.gameObject.GetComponent<BuildPanel>();
            if (panel != null)
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
            return;
        }

        transform.parent = defaultParent.parent;
        BuildManager.Instance.EnterToBuildMode(Pattern.Mirage, Pattern);
        Cost.gameObject.SetActive(false);
        Name.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CheckCanDrag() || !BuildManager.Instance.InBuildMode)
        {
            return;
        }
        
        transform.position = eventData.position;

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
            BuildManager.Instance.TryBuildTower();
        }
        else
        {
            BuildManager.Instance.ExitFromBuildMod();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Pattern.IsUpgrade && Player.Instance.Money >= Pattern.Cost)
        {
            BuildManager.Instance.UpgradeTower(Pattern);
            return;
        }
        BuildManager.Instance.BuildOptionsPanel.ShowDescription(Pattern);
    }

    private bool CheckCanDrag()
    {
        return !Pattern.IsUpgrade && Player.Instance.Money >= Pattern.Cost;
    }
}