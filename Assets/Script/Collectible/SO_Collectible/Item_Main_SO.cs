using UnityEngine;

[CreateAssetMenu(fileName = "Item_Main_SO", menuName = "Scriptable Objects/Item_Main_SO")]
public class Item_Main_SO : ScriptableObject
{
    public int idx;
    public string itemName;
    public Sprite sprite;
    public int maxItemInStack;
}
