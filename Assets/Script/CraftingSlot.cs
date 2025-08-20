using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CraftingSlot : MonoBehaviour
{
    [SerializeField] Recipes recipe;
    [SerializeField] GameObject slotCraft;
    [SerializeField] GameObject plusPrefab;
    [SerializeField] GameObject equalPrefab;

    private void Start()
    {
        CreateNewSlot(recipe);
    }

    void CreateNewSlot(Recipes newRecipes)
    {
        recipe = newRecipes;

        foreach(Transform child in transform)
        {
            Destroy(child);
        }

        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            GameObject newItem = Instantiate(slotCraft, transform);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = recipe.ingredients[i].item.sprite;
            newItem.transform.GetChild(1).GetComponent<TMP_Text>().text = recipe.ingredients[i].count.ToString();
            if(i < recipe.ingredients.Length - 1)
            {
                GameObject _plus = Instantiate(plusPrefab, transform);
            }
        }
        GameObject _equal = Instantiate(equalPrefab, transform);

        GameObject _result = Instantiate(slotCraft, transform);
        _result.transform.GetChild(0).GetComponent<Image>().sprite = recipe.output.sprite;
        _result.transform.GetChild(1).GetComponent<TMP_Text>().text = recipe.outputCount.ToString();
    }


}
