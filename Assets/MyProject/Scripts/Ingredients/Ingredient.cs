using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientData data;

    float timer;
    bool decaying;

    public void StartDecay()
    {
        decaying = true;
        timer = 10f;
    }

    public void StopDecay()
    {
        decaying = false;
    }

    void Update()
    {
        if (!decaying) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
            Destroy(gameObject);
    }
}