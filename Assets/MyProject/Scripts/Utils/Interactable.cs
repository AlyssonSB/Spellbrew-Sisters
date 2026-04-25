    using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Renderer rend;
    public Sprite interactionSprite;
    Color originalColor;

    public virtual bool CanInteract(PlayerInteraction player)
    {
        return true;
    }
    public abstract void Interact(PlayerInteraction player);
    void Start()
    {
        if (rend != null)
            originalColor = rend.material.color;
    }

    public void Highlight(bool state)
    {
        if (rend == null) return;

        rend.material.color = state ? Color.yellow : originalColor;
    }
}