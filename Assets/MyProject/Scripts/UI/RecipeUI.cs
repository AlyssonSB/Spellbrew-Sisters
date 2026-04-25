using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RecipeUI : MonoBehaviour
{
    public Image bottleImage;
    public Image temperatureImage;
    public Image speedImage;
    public GameObject ingredientList;
    public Image ingredientImage;
    public TMP_Text nameText;

    public Sprite[] temperatureSprites;
    public Sprite[] speedSprites;


    private PotionData potion;


    public void Setup(PotionData data)
    {
        potion = data;

        nameText.text = data.potionName;
        bottleImage.color = data.color;
        List<IngredientData> ingredients = data.requiredIngredients;

        foreach (IngredientData ingredient in ingredients)
        {
            ingredientImage.sprite = ingredient.icon;
            Instantiate(ingredientImage, ingredientList.transform);
        }

        switch (data.requiredSpeed)
        {
            case -1:
                speedImage.sprite = speedSprites[0];
                break;
            case 0:
                speedImage.sprite = speedSprites[1];
                break;
            case 1:
                speedImage.sprite = speedSprites[2];
                break;
        }

        switch (data.requiredTemperature)
        {
            case -1:
                temperatureImage.sprite = temperatureSprites[0];
                break;
            case 0:
                temperatureImage.sprite = temperatureSprites[1];
                break;
            case 1:
                temperatureImage.sprite = temperatureSprites[2];
                break;
        }

    }
}