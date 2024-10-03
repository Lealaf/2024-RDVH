using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CameraPosition", order = 1)]
public class CameraPosition : ScriptableObject
{
    public Vector3 position;
    public Vector3 aim;
}
