using UnityEngine;

public class GroundItem : Interactable
{
    public override void Interact(PlayerInteraction player)
    {
        if (player.heldItem != null) return;

        player.PickItem(gameObject);
    }
    public override bool CanInteract(PlayerInteraction player)
    {
        return player.heldItem == null;

    }
}