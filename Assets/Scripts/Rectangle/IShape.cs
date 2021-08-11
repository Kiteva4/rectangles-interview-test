using System;
using UnityEngine;

public interface IShape
{
    Draggable Draggable { get; set; }
    Transform SelfTransform { get; set; }
    IShape Pair { get; set; }
    event Action<IShape> PairAdded; 
    event Action BeforePairRemove; 
    void AddPair(IShape pair);
    void RemovePair();
}
