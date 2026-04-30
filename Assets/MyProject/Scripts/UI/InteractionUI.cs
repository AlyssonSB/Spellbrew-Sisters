using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.AudioSettings;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;

    [Header("Botão principal")]
    public GameObject interactButton;
    public TMP_Text interactText;
    public Image interactIcon;

    [Header("Botões do caldeirão")]
    public GameObject mixButton;
    public GameObject dropButton;

    /*[Header("Botões de poderes")]
    public GameObject elementalButton;
    public GameObject temporalButton;
    private bool elemental = true;
    private bool temporal = false;*/

    [Header("Teclas dos Botões")]
    public GameObject[] buttonsKeys;

    void Awake()
    {
        Instance = this;

        bool isMobile = Application.isMobilePlatform;
        if (isMobile)
        {
            for (int i = 0; i < buttonsKeys.Length; i++)
            {
                buttonsKeys[i].SetActive(false);
            }
        }
    }

    public void UpdateUI(PlayerInteraction player, Interactable interactable)
    {
        HideAll();

        if (interactable == null) return;

        if (interactable.CanInteract(player))
        {
            interactButton.SetActive(true);
            interactIcon.sprite = interactable.interactionSprite;
            interactText.text = "";
        }

        if (interactable is Cauldron cauldron)
        {
            UpdateCauldronButtons(player, cauldron);

            if (cauldron.HasPotion())
            {
                int current = cauldron.GetRemainingAmount();
                int max = cauldron.GetMaxAmount();

                interactText.text = current + "/" + max;
                // cor dinâmica
                // interactText.color = Color.Lerp(Color.red, Color.white, (float)current / max);
            }
        }
    }
    void HideAll()
    {
        interactButton.SetActive(false);
        mixButton.SetActive(false);
    }
    void UpdateCauldronButtons(PlayerInteraction player, Cauldron c)
    {
        if (c.IsMixing()) return;

        if (c.HasIngredients())
            mixButton.SetActive(true);
    }

}