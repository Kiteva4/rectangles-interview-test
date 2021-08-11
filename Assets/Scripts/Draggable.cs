using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler
{
    public event Action<Vector3> Dragged;
    
    private Transform _selfTransform;
    private Camera _camera;
    private Vector3 _screenCenter;
    private Vector3 _worldCenterPosition;

    private void Start()
    {
        _selfTransform = transform;
        _camera = Camera.main;
        _screenCenter = new Vector3(Screen.width* 0.5f, Screen.height* 0.5f, 1f);
        _worldCenterPosition = _camera.ScreenToWorldPoint(_screenCenter);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var screenTouch = _screenCenter + new Vector3(eventData.delta.x, eventData.delta.y, 0f);
        var worldTouchPosition = _camera.ScreenToWorldPoint(screenTouch);
        var worldDelta = worldTouchPosition - _worldCenterPosition;
        var position = _selfTransform.position;

        position = new Vector3(position.x + worldDelta.x, position.y + worldDelta.y, position.z);
        
        TryApplyDeltaPosition(position);
    }

    private void TryApplyDeltaPosition(Vector3 position)
    {
        var colliders = Physics.OverlapBox(position, new Vector3(2.0f, 1.0f, 0.1f) * 0.5f);
        if (colliders.Length <= 1)
        {
            _selfTransform.position = position;
            Dragged?.Invoke(position);
        }
    }
}