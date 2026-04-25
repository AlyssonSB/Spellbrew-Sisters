using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Potion", menuName = "Game/Potion")]
public class PotionData : ScriptableObject
{
    public string potionName;
    public Color color;
    public List<IngredientData> requiredIngredients;
    public int requiredTemperature; 
    public int requiredSpeed;

    [Header("Quantidade gerada")]
    public int yieldAmount = 3; // quantos potes essa receita enche
}