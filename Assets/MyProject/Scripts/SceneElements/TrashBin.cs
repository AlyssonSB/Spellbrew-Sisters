using UnityEngine;

public class TrashBin : Interactable
{
    public override void Interact(PlayerInteraction player)
    {
        if (player.heldItem == null) return;

        Destroy(player.heldItem);
        player.heldItem = null;



        Debug.Log("Item descartado");
    }
}