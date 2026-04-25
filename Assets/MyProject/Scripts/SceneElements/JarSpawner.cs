using UnityEngine;
using static PlayerInteraction;

public class JarSpawner : Interactable
{
    public GameObject jarPrefab;

    public override void Interact(PlayerInteraction player)
    {
        if (player.heldItem != null) return;

        GameObject jar = Instantiate(jarPrefab);
        player.PickItem(jar);
    }

    public override bool CanInteract(PlayerInteraction player)
    {
        return player.heldItem == null;
    }
}