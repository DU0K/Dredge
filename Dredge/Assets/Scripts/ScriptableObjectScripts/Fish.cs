using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "Scriptable Objects/Fish")]
public class Fish : ScriptableObject
{
    public MainInfo mainInfo;
    public float size;
    public string Condition;
    public string Type;
}
