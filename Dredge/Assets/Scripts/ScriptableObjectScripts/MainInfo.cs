using UnityEngine;

[CreateAssetMenu(fileName = "MainInfo", menuName = "Scriptable Objects/MainInfo")]
public class MainInfo : ScriptableObject
{
    public int ID;
    public string itemName;
    public Sprite itemIcon;
    public int itemWidth;
    public int itemHeight;

}
