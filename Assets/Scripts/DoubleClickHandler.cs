using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public event Action DoubleClicked;
    
    [SerializeField, Range(0.01f, 1.0f)]
    private float _doubleClickDelay = 0.3f;
    private float _previousClickedTime = 0f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickTime - _previousClickedTime <= _doubleClickDelay)
            DoubleClicked?.Invoke();
        
        _previousClickedTime = eventData.clickTime;
    }
}
