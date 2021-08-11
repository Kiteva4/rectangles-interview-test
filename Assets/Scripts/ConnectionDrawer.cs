using UnityEngine;

public class ConnectionDrawer : MonoBehaviour
{
    [SerializeField] 
    private LineRenderer lineRenderer;

    private IShape _shape;

    private void Awake()
    {
        if (TryGetComponent<Rectangle>(out var rectangle))
        {
            _shape = rectangle;
        }
        else
        {
            Debug.LogError("Cant find Rectangle component on this gameobject");
        }
        
        lineRenderer.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _shape.PairAdded += OnPairAdded;
        _shape.BeforePairRemove += OnPairRemove;
        _shape.Draggable.Dragged += OnRectangleDragged;
        
    }

    private void OnDisable()
    {
        _shape.PairAdded -= OnPairAdded;
        _shape.BeforePairRemove -= OnPairRemove;
        _shape.Draggable.Dragged -= OnRectangleDragged;
        
    }
    
    private void OnPairAdded(IShape pair)
    {
        lineRenderer.gameObject.SetActive(true);
        lineRenderer.SetPosition(1, pair.SelfTransform.position);
        lineRenderer.SetPosition(0, _shape.SelfTransform.position);
        _shape.Pair.Draggable.Dragged += OnRectanglePairDragged;
    }
    
    private void OnPairRemove()
    {
        lineRenderer.gameObject.SetActive(false);
        _shape.Pair.Draggable.Dragged -= OnRectanglePairDragged;
    }

    private void OnRectangleDragged(Vector3 vector3) => lineRenderer.SetPosition(0, vector3);
    private void OnRectanglePairDragged(Vector3 vector3) => lineRenderer.SetPosition(1, vector3);
}
