using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StorageUI : MonoBehaviour
{
    public static StorageUI Instance;

    [Header("UI")]
    public GameObject panel;
    public Transform contentParent;
    public GameObject slotPrefab;
    public TMP_Text capacityText;

    private StorageStation currentStation;
    private PlayerInteraction currentPlayer;

    private List<StorageSlotUI> slots = new List<StorageSlotUI>();

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Open(StorageStation station, PlayerInteraction player)
    {
        currentStation = station;
        currentPlayer = player;
        Cursor.lockState = CursorLockMode.None;
        panel.SetActive(true);

        GenerateSlots();
        Refresh();
    }

    public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
        if (currentStation != null)
            currentStation.ClearCurrentPlayer();
    }

    void GenerateSlots()
    {
        if (slots.Count > 0) return;

        foreach (PotionData potion in currentStation.allPotions)
        {
            GameObject obj = Instantiate(slotPrefab, contentParent);

            StorageSlotUI slot = obj.GetComponent<StorageSlotUI>();
            slot.Setup(potion, this);

            slots.Add(slot);
        }
    }
    public bool IsOpen()
    {
        return panel.activeSelf;
    }

    public void Refresh()
    {
        if (currentStation == null) return;

        foreach (var slot in slots)
        {
            if (slot != null)
                slot.Refresh(currentStation);
        }

        if (capacityText != null)
            capacityText.text = currentStation.GetTotal() + "/" + currentStation.GetMax();
    }


    public void OnClickPotion(PotionData potion)
    {
        if (currentPlayer.heldItem != null)
        {
            Debug.Log("Mãos ocupadas!");
            return;
        }

        currentStation.Take(potion, currentPlayer);

        Refresh();
        Close(); // fecha automático
    }
}