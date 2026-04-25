using System.Collections.Generic;
using UnityEngine;

public class StorageStation : Interactable
{
    [Header("Config")]
    public List<PotionData> allPotions;
    public int maxCapacity = 10;
    public GameObject jarPrefab;
    private PlayerInteraction currentPlayer;
    private Dictionary<PotionData, int> storedPotions = new Dictionary<PotionData, int>();
    private int currentTotal = 0;

    public override void Interact(PlayerInteraction player)
    {
        var held = player.heldItem;

        // =========================
        // GUARDAR
        // =========================
        if (held != null)
        {
            var jar = held.GetComponent<JarContent>();

            if (jar == null || !jar.IsFilled()) return;

            if (currentTotal >= maxCapacity)
            {
                Debug.Log("Armário cheio!");
                return;
            }

            Store(jar.potionData);

            Destroy(player.heldItem);
            player.heldItem = null;

            if (StorageUI.Instance != null && StorageUI.Instance.IsOpen())
            {
                StorageUI.Instance.Refresh();
            }

            return;
        }

        // =========================
        // ABRIR UI
        // =========================
        StorageUI.Instance.Open(this, player);
        currentPlayer = player;
    }

    public override bool CanInteract(PlayerInteraction player)
    {
        return true;
    }

    // =========================
    // LÓGICA
    // =========================

    public void Store(PotionData potion)
    {
        if (!storedPotions.ContainsKey(potion))
            storedPotions[potion] = 0;

        storedPotions[potion]++;
        currentTotal++;
    }
    public void ClearCurrentPlayer()
    {
        currentPlayer = null;
    }

    public bool CanTake(PotionData potion)
    {
        return storedPotions.ContainsKey(potion) && storedPotions[potion] > 0;
    }

    public void Take(PotionData potion, PlayerInteraction player)
    {
        if (!CanTake(potion)) return;
        if (player.heldItem != null)
        {
            Debug.Log("Mãos ocupadas!");
            return;
        }

        storedPotions[potion]--;
        currentTotal--;

        if (storedPotions[potion] <= 0)
            storedPotions.Remove(potion);

        GameObject jar = Instantiate(jarPrefab);
        jar.GetComponent<JarContent>().Fill(potion.color, potion);

        player.PickItem(jar);
    }

    // =========================
    // GETTERS PRA UI
    // =========================

    public int GetAmount(PotionData potion)
    {
        if (!storedPotions.ContainsKey(potion)) return 0;
        return storedPotions[potion];
    }

    public int GetTotal() => currentTotal;
    public int GetMax() => maxCapacity;

    private void OnTriggerExit(Collider other)
    {
        PlayerInteraction player = other.GetComponent<PlayerInteraction>();

        if (player != null && player == currentPlayer)
        {
            StorageUI.Instance.Close();
            currentPlayer = null;
        }
    }
}