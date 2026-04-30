    using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public GameObject interactZone;
    public Sprite interactionSprite;

    public virtual bool CanInteract(PlayerInteraction player)
    {
        return true;
    }
    public abstract void Interact(PlayerInteraction player);
    void Start()
    {
        interactZone.SetActive(false);
    }

    public void Highlight(bool state)
    {
        if (interactZone == null) return;

        interactZone.SetActive(state);
    }
}