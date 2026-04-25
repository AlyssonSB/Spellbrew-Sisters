using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cauldron : Interactable
{
    public Renderer liquid;

    public List<PotionData> allPotions;
    private List<IngredientData> currentIngredients = new List<IngredientData>();
    private bool isMixing = false;

    public Transform ingredientUIParent;
    public GameObject ingredientIconPrefab;

    private PotionData currentPotion = null;
    private bool hasPotion = false;
    int maxIngredients = 3;
    public float mixTime = 3f;

    public int temperatureState = 0;
    public int speedState = 0;

    public ParticleSystem currentParticles;
    private ParticleSystem.MainModule main;
    private int remainingPotionAmount = 0;

    GameObject player;
    Animator animator; 

    void Start()
    {
        main = currentParticles.main;

        player = GameObject.FindWithTag("Player");
        animator = player.GetComponent<Animator>();
    }
    public void AddHeat()
    {
        if (temperatureState < 1)
            temperatureState++;

        UpdateTemperatureEffects();
    }

    public void ResetEffects()
    {

        temperatureState = 0;
        speedState = 0;
        UpdateSpeedEffects();
        UpdateTemperatureEffects();
    }
    public void AddCold()
    {
        if (temperatureState > -1)
            temperatureState--;

        UpdateTemperatureEffects();
    }

    public void AddSpeed()
    {
        if (speedState < 1)
            speedState++;

        UpdateSpeedEffects();
    }

    public void AddSlow()
    {
        if (speedState > -1)
            speedState--;

        UpdateSpeedEffects();
    }

    void UpdateTemperatureEffects()
    {
        Color smokeColor = Color.gray;

        if (temperatureState == 1) smokeColor = Color.red;
        if (temperatureState == -1) smokeColor = Color.cyan;

        main.startColor = smokeColor;
    }

    void UpdateSpeedEffects()
    {
        float speed = 5;

        if (speedState == 1) speed = 15;
        if (speedState == -1) speed = 1;

        main.startSpeed = speed;
    }
    public int GetRemainingAmount()
    {
        return remainingPotionAmount;
    }

    public int GetMaxAmount()
    {
        if (currentPotion == null) return 0;
        return currentPotion.yieldAmount;
    }


    public void AddSpecialIngredient(IngredientData data)
    {
        if (isMixing || hasPotion) return;
        if (currentIngredients.Contains(data)) return;

        currentIngredients.Add(data);

        Debug.Log("Ingrediente especial: " + data.ingredientName);

    }

    void AddIngredientVisual(IngredientData data)
    {
        GameObject icon = Instantiate(ingredientIconPrefab, ingredientUIParent);

        Image img = icon.GetComponent<Image>();
        img.sprite = data.icon;
    }
    public override void Interact(PlayerInteraction player)
    {
        if (isMixing) return;

        var held = player.heldItem;

        if (hasPotion)
        {
            if (held == null) return;

            var jar = held.GetComponent<JarContent>();
            if (jar == null || jar.IsFilled()) return;

            jar.Fill(currentPotion.color, currentPotion);

            remainingPotionAmount--;

            if (remainingPotionAmount <= 0)
            {
                hasPotion = false;
                currentPotion = null;
                liquid.material.color = Color.blue;
            }

            return;
        }

        if (held != null)
        {
            var ing = held.GetComponent<Ingredient>();

            if (ing != null)
            {
                if (currentIngredients.Count >= maxIngredients)
                    return;

                currentIngredients.Add(ing.data);
                AddIngredientVisual(ing.data);

                Destroy(player.heldItem);
                player.heldItem = null;
            }
        }
    }

    void ClearUI()
    {
        foreach (Transform child in ingredientUIParent)
            Destroy(child.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isMixing && currentIngredients.Count > 0)
        {
            Debug.Log("Iniciando mistura...");
            StartCoroutine(Mix());
        }
    }

    public void MixButton()
    {
        if (!isMixing && currentIngredients.Count > 0)
        {
            Debug.Log("Iniciando mistura...");
            StartCoroutine(Mix());
        }
    }

    IEnumerator Mix()
    {
        isMixing = true;

        yield return new WaitForSeconds(mixTime);


        PotionData result = CheckRecipe();

        if (result != null)
        {
            Debug.Log("POÇÃO CORRETA: " + result.potionName);

            currentPotion = result;
            remainingPotionAmount = result.yieldAmount;
            hasPotion = true;

            liquid.material.color = result.color;
            main.startColor = result.color;
            currentIngredients.Clear();

        }
        else
        {
            Debug.Log("ERROU A RECEITA → STUN");

            StartCoroutine(StunPlayer());

            currentIngredients.Clear();
            liquid.material.color = Color.blue;
        }

        isMixing = false;
        ClearUI();
    }

    PotionData CheckRecipe()
    {
        foreach (var potion in allPotions)
        {
            // Ingredientes batem?
            if (!CompareLists(potion.requiredIngredients, currentIngredients))
                continue;

            // Temperatura bate?
            if (potion.requiredTemperature != temperatureState)
                continue;

            // Velocidade bate?
            if (potion.requiredSpeed != speedState)
                continue;

            return potion;
        }

        return null;
    }

    bool CompareLists(List<IngredientData> a, List<IngredientData> b)
    {
        if (a.Count != b.Count) return false;

        List<IngredientData> temp = new List<IngredientData>(b);

        foreach (var item in a)
        {
            if (!temp.Contains(item))
                return false;

            temp.Remove(item);
        }

        return true;
    }

    IEnumerator StunPlayer()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        PlayerInteraction playerInteraction = player.GetComponent<PlayerInteraction>();

        if (player != null)
        {
            animator.SetTrigger("Stunned");
            
            Debug.Log("Player stunado por 5s");

            playerController.enabled = false;
            playerInteraction.enabled = false;

            yield return new WaitForSeconds(5f);

            playerInteraction.enabled = true;
            playerController.enabled = true;

            Debug.Log("Player voltou");
        }

    }

    public override bool CanInteract(PlayerInteraction player)
    {
        if (isMixing) return false;

        // Sempre pode interagir com o caldeirão
        return true;
    }

    public bool HasIngredients()
    {
        return currentIngredients.Count > 0;
    }

    public bool HasPotion()
    {
        return currentPotion != null && remainingPotionAmount > 0;
    }

    public bool IsMixing()
    {
        return isMixing;
    }
}