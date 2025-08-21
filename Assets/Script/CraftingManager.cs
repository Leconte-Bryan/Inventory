using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CraftingManager : MonoBehaviour
{
    public RecipeType selectedRecipeType = RecipeType.BASIC;

    [SerializeField] Recipes_SO[] recipesSO;
    [SerializeField] GameObject recipePrefab;
    [SerializeField] Transform recipeParent;
    [SerializeField] InventorySystem inventory;

    private void Start()
    {
        UpdateRecipeUI();
    }

    public void SelectRecipeType(string type)
    {
        switch (type)
        {
            case "WEAPON":
                selectedRecipeType = RecipeType.WEAPON;
                break;
            case "CONSOMMABLE":
                selectedRecipeType = RecipeType.CONSOMMABLE;
                break;
            case "ARMOR":
                selectedRecipeType = RecipeType.ARMOR;
                break;
            case "BASIC":
                selectedRecipeType = RecipeType.BASIC;
                break;
        }
        UpdateRecipeUI();
    }

    public bool CanCraftRecipe(Recipes_SO recipeSo)
    {
        int itemFound = 0;
        int itemToFind = recipeSo.ingredients.Length;
        foreach (Ingredient ingredient in recipeSo.ingredients)
        {
            foreach (Ingredient _ingredient in inventory.ingredients)
            {
                if (_ingredient.item == ingredient.item && _ingredient.count >= ingredient.count)
                {
                    itemFound++;
                    break;
                }
            }
        }
        return itemFound == itemToFind ? true : false;
    }

    public void UpdateRecipeUI()
    {
        foreach (Transform child in recipeParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < recipesSO.Length; i++)
        {
            if (recipesSO[i].recipeType == selectedRecipeType)
            {
                GameObject newRecipe = Instantiate(recipePrefab, recipeParent);
                newRecipe.name = recipesSO[i].name;
            }
        }

        for (int i = 0; i < recipeParent.childCount; i++)
        {
            ItemRecipe recipeScript = recipeParent.GetChild(i).GetComponent<ItemRecipe>();
            Debug.Log(recipeScript.gameObject.name);
            Recipes_SO _recipeSo = null;

            foreach (Recipes_SO r in recipesSO)
            {
                Debug.Log("name is" + r.output.itemName);

                if (r.recipeName == recipeParent.GetChild(i).name)
                {
                    _recipeSo = r;
                    Debug.Log(_recipeSo);
                    break;
                }
            }
            if (recipeScript)
            {
                recipeScript.CreateNewSlot(_recipeSo);
            }
        }
    }
}
