using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PenCanvas : MonoBehaviour, IPointerClickHandler
{
    public Action OnPenCanvasLeftClickEvent;
    public Action OnPenCanvasRightClickEvent;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerId == -1)       // Left click
        {
            OnPenCanvasLeftClickEvent? .Invoke();
        }
        else if(eventData.pointerId == -2)
        {
            OnPenCanvasRightClickEvent? .Invoke();
        }
    }
}
