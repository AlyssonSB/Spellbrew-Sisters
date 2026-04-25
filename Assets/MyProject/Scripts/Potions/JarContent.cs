using UnityEngine;

public class JarContent : MonoBehaviour
{
    public Renderer rend;
    public ParticleSystem currentParticle;
    private bool filled = false;
    public PotionData potionData;

    public void Fill(Color color, PotionData potion)
    {
        var main = currentParticle.main;

        rend.material.color = color;
        currentParticle.gameObject.SetActive(true);
        main.startColor = color;
        potionData = potion;
        filled = true;
    }

    public bool IsFilled()
    {
        return filled;
    }
}