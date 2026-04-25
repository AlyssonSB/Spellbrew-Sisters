using UnityEngine;
using static PlayerInteraction;

public class Box : Interactable
{
    public GameObject ingredientPrefab;

    public override void Interact(PlayerInteraction player)
    {
        if (player.heldItem != null) return;

        GameObject item = Instantiate(ingredientPrefab);
        player.PickItem(item);
    }

    public override bool CanInteract(PlayerInteraction player)
    {
        return player.heldItem == null;
    }
}