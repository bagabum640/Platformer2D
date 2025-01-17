using UnityEngine;
using UnityEngine.UI;

public class HealthSliderBar : HealthBar
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] protected Slider Slider;

    private void Awake()
    {
        Slider.maxValue = Health.MaxAmount;       
    }

    private void FixedUpdate()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offset);
    }

    public override void UpdateHealthAmount()
    {
        if (Health.IsAlive != true)
        {
            Slider.gameObject.SetActive(false);
        }
        else
        {
            Slider.value = Health.CurrentAmount;
            Slider.gameObject.SetActive(true);
        }
    }
}