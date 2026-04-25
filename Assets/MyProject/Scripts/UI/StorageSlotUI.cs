using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorageSlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text amountText;
    public Button button;

    private PotionData potion;
    private StorageUI ui;

    public void Setup(PotionData data, StorageUI storageUI)
    {
        potion = data;
        ui = storageUI;

        icon.color = data.color;
        nameText.text = data.potionName;
        button.onClick.AddListener(() => ui.OnClickPotion(potion));
    }

    public void Refresh(StorageStation station)
    { 
        int amount = station.GetAmount(potion);

        amountText.text = amount.ToString();
        button.interactable = amount > 0;
    }
}