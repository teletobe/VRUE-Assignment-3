using UnityEngine;

public class PositionAndRotation
{
    public Vector3 position;
    public Quaternion rotation;

    public PositionAndRotation(Vector3 _position, Quaternion _rotation)
    {
        rotation = _rotation;
        position = _position;
    }
}
