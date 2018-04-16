using UnityEngine;

public class PlayerPointInTime
{

    public Vector3 position;
    public Quaternion rotation;

    public PlayerPointInTime(Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }

}
