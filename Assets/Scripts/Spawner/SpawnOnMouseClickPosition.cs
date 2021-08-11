using System;
using System.Collections;
using UnityEngine;

public class SpawnOnMouseClickPosition : ISpawnPointGenerator
{
    public event Action<Vector3> SpawnOnPoint;

    private Camera _mainCamera;
    public SpawnOnMouseClickPosition(MonoBehaviour parent)
    {
        _mainCamera = Camera.main;
        Initialize(parent);
    }

    public void Initialize(MonoBehaviour parent) => parent.StartCoroutine(CheckSpawnConditions());

    private IEnumerator CheckSpawnConditions()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0) && IsEmptySpace())
            {
                var pos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                SpawnOnPoint?.Invoke(new Vector3(pos.x, pos.y, 0.0f));
            }
            
            yield return null;
        }
    }

    private bool IsEmptySpace()
    {
        var isEmpty = false;
        
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out _)) 
            isEmpty = true;

        return isEmpty;
    }
}
