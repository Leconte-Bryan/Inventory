using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemRecipe : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public Recipes_SO recipe;
    [SerializeField] GameObject slotCraft;
    [SerializeField] GameObject plusPrefab;
    [SerializeField] GameObject equalPrefab;

    /// <summary>
    /// Display
    /// </summary>
    /// <param name="newRecipes"></param>
    public void CreateNewSlot(Recipes_SO newRecipes)
    {
        recipe = newRecipes;
        Debug.Log(recipe);
        foreach (Transform child in transform)
        {
            Destroy(child);
        }

        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            GameObject newItem = Instantiate(slotCraft, transform);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = recipe.ingredients[i].item.sprite;
            newItem.transform.GetChild(1).GetComponent<TMP_Text>().text = recipe.ingredients[i].count.ToString();
            if (i < recipe.ingredients.Length - 1)
            {
                GameObject _plus = Instantiate(plusPrefab, transform);
            }
        }
        GameObject _equal = Instantiate(equalPrefab, transform);

        GameObject _result = Instantiate(slotCraft, transform);
        _result.transform.GetChild(0).GetComponent<Image>().sprite = recipe.output.sprite;
        _result.transform.GetChild(1).GetComponent<TMP_Text>().text = recipe.outputCount.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        craftRecipe();
    }

    void craftRecipe()
    {
        GameEvents.TryCraftingItem?.Invoke(recipe);
    }

}
