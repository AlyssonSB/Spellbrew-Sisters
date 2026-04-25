using UnityEngine;
using UnityEngine.UI;

public class RatingManager : MonoBehaviour
{
    public static RatingManager Instance;

    public float rating = 5f;
    public float maxRating = 5f;
    float blinkTimer;
    public Image ratingFill; 

    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        HandleBlink();
    }
    void HandleBlink()
    {
        Color c = ratingFill.color;

        c.a = Mathf.Lerp(0.3f, 1f, (Mathf.Sin(blinkTimer) + 1f) / 2f);

        ratingFill.color = c;

        // quanto menor o rating, mais rápido pisca
        float speed = Mathf.Lerp(2f, 8f, 1f - (rating / 1.5f));

        blinkTimer += Time.deltaTime * speed;

    }

    public void AddRating(float amount)
    {
        rating += amount;
        rating = Mathf.Clamp(rating, 0, maxRating);

        UpdateUI();

        if (rating <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        ratingFill.fillAmount = rating / maxRating;
        ratingFill.color = Color.Lerp(Color.red, Color.green, rating / maxRating);
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");

    }
}