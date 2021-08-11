using UnityEngine;

[CreateAssetMenu(fileName = "RectangleData", menuName = "Create/Create rectanble data", order = 0)]
public class RectangleData : ScriptableObject
{
    public Rectangle rectangle;
    public Vector3 Size = new Vector3(2.0f, 1.0f, 0.1f);
}
