using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Entity entity;
    private CharacterStats stats;
    private RectTransform rectTransform;
    private Slider slider;

    private void Start()
    {
        rectTransform = GetComponentInChildren<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        entity = GetComponentInParent<Entity>();
        stats = GetComponentInParent<CharacterStats>();
        if (entity != null)
            entity.onFlipped += FlipUI;

        if (stats != null)
            stats.onHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        if (entity != null)
            entity.onFlipped -= FlipUI;

        if (stats != null)
            stats.onHealthChanged -= UpdateHealthUI;
    }

    private void FlipUI() => rectTransform.Rotate(0f, 180f, 0f);
    private void UpdateHealthUI()
    {
        slider.maxValue = stats.maxHP.GetValue();
        slider.value = stats.currentHP;
    }
    

}
