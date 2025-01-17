﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthSliderBar : HealthSliderBar
{
    [SerializeField] private float _fillingSpeed = 10f;

    private Coroutine _coroutine;

    public override void UpdateHealthAmount()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (Health.IsAlive != true)
        {
            this.Slider.gameObject.SetActive(false);
        }
        else
        {
            _coroutine = StartCoroutine(SmoothFill());
            this.Slider.gameObject.SetActive(true);
        }     
    }

    private IEnumerator SmoothFill()
    {
        while (Slider.value != Health.CurrentAmount)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, Health.CurrentAmount, _fillingSpeed * Time.deltaTime);

            yield return null;
        }
    }
}