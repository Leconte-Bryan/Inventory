using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Crafting : MonoBehaviour
{
    // The object AND his components
    [SerializeField] List<Recipes_SO> recipes;

    [SerializeField] Item_Main_SO steel;
    [SerializeField] Item_Main_SO wood;
    [SerializeField] Item_Main_SO sword;


    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown("2"))
        {
            CraftItem();
        }
    }


    void CraftItem()
    {
        Debug.Log(recipes[0]);
    }
    

    /*
    string SaveToJson()
    {
        var recipesData = JsonConvert.SerializeObject(recipe);
        string filePath = Application.dataPath + "/recipes.txt";
        File.WriteAllText(filePath, recipesData);
        return recipesData;
    }

    void LoadFromJson()
    {
        string path = Application.dataPath + "/recipes.txt";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            recipe =  JsonConvert.DeserializeObject<Dictionary<string, List<Ingredient>>>(json);
            foreach(string elem in recipe.Keys)
            {
                Debug.Log(elem);
            }
        }
        else
        {
            Debug.LogWarning("JSON file not found at: " + path);
        }
    }
    */
}
