using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DotController : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public LineController line;
    public int index;

    public Action<DotController> OnDragEvent;
    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent? .Invoke(this);
    }

    public Action<DotController> OnRightClickEvent;
    public Action<LineController> OnLeftClickEvent;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerId == -2)   // Right click
        {
            OnRightClickEvent? .Invoke(this);
        }
        else if(eventData.pointerId == -1)
        {
            OnLeftClickEvent? .Invoke(line);
        }
    }

    public void SetLine(LineController line)
    {
        this.line = line;
    }
}
