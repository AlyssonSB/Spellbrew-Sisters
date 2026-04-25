using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryZone : Interactable
{

    public override void Interact(PlayerInteraction player)
    {
        if (player.heldItem == null) return;

        var held = player.heldItem;

        var jar = held.GetComponent<JarContent>();

        if (jar == null || !jar.IsFilled()) return;

        if (jar.potionData == null)
        {
            Debug.Log("Pote sem poção válida!");
            return;
        }

        bool success = OrderManager.Instance.TryCompleteOrder(jar.potionData);
        if (success)
        {
            Debug.Log("Entrega correta!");
        }
        else
        {
            Debug.Log("Entrega errada!");
            // opcional: penalidade
        }

        Destroy(player.heldItem);
        player.heldItem = null;
    }
}