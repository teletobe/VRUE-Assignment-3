using UnityEngine;

public class PositionAndRotationAndScale
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public PositionAndRotationAndScale(Vector3 _position, Quaternion _rotation, Vector3 _scale)
    {
        rotation = _rotation;
        position = _position;
        scale = _scale;
    }
}