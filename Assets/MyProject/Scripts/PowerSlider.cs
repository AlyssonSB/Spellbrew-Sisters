using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{
    private Animator animator;
    public Sprite[] sliderSprites;
    public Image sliderActualSprite;
    public Slider slider;
    void Start()
    {
        animator = GetComponent<Animator>();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (slider.value == -1)
        {
            sliderActualSprite.sprite = sliderSprites[0];
            animator.SetTrigger("Power1");
        }
        else if (slider.value == 0)
        {
            sliderActualSprite.sprite = sliderSprites[1];
            animator.SetTrigger("Neutral");
        }
        else
        {
            sliderActualSprite.sprite = sliderSprites[2];
            animator.SetTrigger("Power2");
        }

    }
}
