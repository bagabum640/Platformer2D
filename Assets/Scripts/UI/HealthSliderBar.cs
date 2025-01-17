using UnityEngine;
using UnityEngine.UI;

public class HealthSliderBar : HealthBar
{
    [SerializeField] protected Slider Slider;
    [SerializeField] private Vector3 _offset;

    private Camera _mainCamera;

    private void Awake()
    {
        Slider.maxValue = Health.MaxAmount;      
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Slider.transform.position = _mainCamera.WorldToScreenPoint(transform.parent.position + _offset);
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