using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderUI : MonoBehaviour
{
    public Image bottleImage;
    public TMP_Text nameText;
    public Slider timerSlider;

    public PotionData potion;

    private float timeLeft;
    private float maxTime = 45f;

    private OrderManager manager;

    public void Setup(PotionData data, OrderManager m)
    {
        potion = data;
        manager = m;

        nameText.text = data.potionName;
        bottleImage.color = data.color;

        timeLeft = maxTime;
        timerSlider.maxValue = maxTime;
        timerSlider.value = maxTime;
    }

    public float GetRatingValue()
    {
        float percent = timeLeft / maxTime;

        if (percent > 0.66f) return 0.5f;   // muito rápido (5 estrelas)
        if (percent > 0.33f) return 0.3f;   // médio (4 estrelas)

        // lento → escala de 0.1 a 0.3
        return Mathf.Lerp(0.1f, 0.3f, percent);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerSlider.value = timeLeft;

        if (timeLeft <= 0)
        {
            manager.FailOrder(this);
        }
    }
}