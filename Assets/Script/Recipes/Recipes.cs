using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "Scriptable Objects/Recipes")]
public class Recipes : ScriptableObject
{
    public Item_Main_SO output;
    public int outputCount;
    public Ingredient[] ingredients;



}
[System.Serializable]
public class Ingredient
{
    public Item_Main_SO item;
    public int count;

    public Ingredient(Item_Main_SO _item, int _count)
    {
        this.item = _item;
        this.count = _count;
    }
}