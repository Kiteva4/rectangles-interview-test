using UnityEngine;

public class RectangleSpawner : MonoBehaviour
{
    [SerializeField] 
    private Transform runtimeObjectsHolder; 
        
    private IShapeFactory _shapeFactory;
    private ISpawnPointGenerator _spawnPointGenerator;

    private RectangleData _rectangleData;
    private Collider[] _colliders = {};
    
    private void Awake()
    {
        _rectangleData = Resources.Load<RectangleData>("RectangleData");
        _spawnPointGenerator = new SpawnOnMouseClickPosition(this);
        _shapeFactory = new RandomColorRectangleFactory(_rectangleData);
    }

    private void OnEnable() => _spawnPointGenerator.SpawnOnPoint += OnNeedSpawnObject;
    private void OnDisable() => _spawnPointGenerator.SpawnOnPoint += OnNeedSpawnObject;

    private void OnNeedSpawnObject(Vector3 spawnPosition)
    {
        _colliders = Physics.OverlapBox(spawnPosition, _rectangleData.Size * 0.5f);
        if(_colliders.Length == 0)
        {
            var rectangle = _shapeFactory.GetShape();
            rectangle.SelfTransform.position = spawnPosition;
            rectangle.SelfTransform.parent = runtimeObjectsHolder;
        }   
        else
        {
            Debug.Log("Not place to instantiate new rectangle");
        }
    }
}
