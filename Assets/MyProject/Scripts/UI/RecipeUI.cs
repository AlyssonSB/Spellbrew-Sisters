using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RecipeUI : MonoBehaviour
{
    public Image bottleImage;
    public GameObject ingredientList;
    public Image ingredientImage;
    public TMP_Text nameText;
    public TMP_Text recipeText;


    private PotionData potion;


    public void Setup(PotionData data)
    {
        potion = data;

        nameText.text = data.potionName;
        recipeText.text = data.potionRecipe;
        bottleImage.color = data.color;
        List<IngredientData> ingredients = data.requiredIngredients;

        foreach (IngredientData ingredient in ingredients)
        {
            ingredientImage.sprite = ingredient.icon;
            Instantiate(ingredientImage, ingredientList.transform);
        }


    }
}