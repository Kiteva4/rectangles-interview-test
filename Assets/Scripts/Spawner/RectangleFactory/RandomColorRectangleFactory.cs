using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class RandomColorRectangleFactory : IShapeFactory
{
    private RectangleData _rectangleData;
    
    public RandomColorRectangleFactory(RectangleData rectangleData) => _rectangleData = rectangleData;

    public IShape GetShape()
    {
        var go = Object.Instantiate(_rectangleData.rectangle);

        if (!go.TryGetComponent<Rectangle>(out var rectangle))
            throw new ArgumentException("Cant find Rectangle component on instantiated object");
        
        rectangle.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        rectangle.SelfTransform.localScale = _rectangleData.Size;
        
        return rectangle;
    }
}
