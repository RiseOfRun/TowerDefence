using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private bool canDrag = true;

    public void OnBeginDrag(PointerEventData eventData)
    {
        canDrag = CheckCanDrag();
        if (canDrag)
        {
            BeginDragFunction(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            DragFunction(eventData);
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            EndDragFunction(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickFunction(eventData);
    }

    public virtual void BeginDragFunction(PointerEventData eventData)
    {
    }

    public virtual void DragFunction(PointerEventData eventData)
    {
    }

    public virtual void EndDragFunction(PointerEventData eventData)
    {
    }

    public virtual void ClickFunction(PointerEventData eventData)
    {
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

    public abstract bool CheckCanDrag();
}