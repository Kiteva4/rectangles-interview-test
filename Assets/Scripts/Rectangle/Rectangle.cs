using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rectangle : MonoBehaviour, IShape, IPointerClickHandler
{
    public static IShape ShapeReadyToPair;
    
    public event Action<IShape> PairAdded;
    public event Action BeforePairRemove;
    public Draggable Draggable { get; set; }
    public Transform SelfTransform { get; set; }
    public IShape Pair { get; set; }
    
    private DoubleClickHandler _doubleClickHandler;

    #region Unity methods
    private void Awake()
    {
        _doubleClickHandler = GetComponent<DoubleClickHandler>();
        Draggable = GetComponent<Draggable>();
        SelfTransform = transform;
    }

    private void OnEnable() => _doubleClickHandler.DoubleClicked += OnDoubleClicked;
    private void OnDisable() => _doubleClickHandler.DoubleClicked -= OnDoubleClicked;
    #endregion
    
    public void AddPair(IShape pair)
    {
        Pair = pair;
        PairAdded?.Invoke(pair);
    }
    
    public void RemovePair()
    {
        if(Pair is null)
            return;
        
        BeforePairRemove?.Invoke();
        
        Pair = null;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftShift) || Pair != null) 
            return;

        PairRectangles();
    }

    private void OnDoubleClicked()
    {
        Pair?.RemovePair();
        RemovePair();
        if (ReferenceEquals(ShapeReadyToPair, this))
            ShapeReadyToPair = null;
        Destroy(gameObject);
    }
    
    private void PairRectangles()
    {
        if (ShapeReadyToPair == null)
        {
            Debug.Log($"<color=green>Save clicked rect as RectangleReadyToPair</color>");
            ShapeReadyToPair = this;
        }
        else if(!ReferenceEquals(ShapeReadyToPair, this))
        {
            ShapeReadyToPair.AddPair(this);
            AddPair(ShapeReadyToPair);
            ShapeReadyToPair = null;
            Debug.Log($"<color=orange>Connect two rectangles</color>");
        }
    }
}
