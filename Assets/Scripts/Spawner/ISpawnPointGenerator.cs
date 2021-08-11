using System;
using UnityEngine;

public interface ISpawnPointGenerator
{
    event Action<Vector3> SpawnOnPoint;
}
