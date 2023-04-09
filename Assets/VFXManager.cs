using System;
using System.Collections;
using System.Collections.Generic;
using Core.Scriptable_Variables;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private FloatVariable _playerHealth;
    [SerializeField] private Volume _volume;
    [SerializeField] private Color _damageColor;
    [SerializeField] private float _damageEffectTime = .2f;
    [SerializeField] private AnimationCurve _damageEffectCurve;

    private Color _vignetteStartColor;
    private float _vignetteTimer;

    private void OnEnable()
    {
        _playerHealth.OnValueChanged += OnPlayerHealthChanged;
    }
    private void OnDisable()
    {
        _playerHealth.OnValueChanged -= OnPlayerHealthChanged;
    }

    private void Start()
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            _vignetteStartColor = vignette.color.value;
        }
    }



    #region Event Listeners

    private void OnPlayerHealthChanged(float changeamount)
    {
        if (changeamount > 0) return;

        DamageTaken();
    }

    private void DamageTaken()
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            StartCoroutine(VignetteEffect(vignette));
        }
    }

    private IEnumerator VignetteEffect(Vignette vignette)
    {
        while (_vignetteTimer <= _damageEffectTime)
        {
             _vignetteTimer += Time.deltaTime;
            vignette.color.value = Color.Lerp(_vignetteStartColor,
            _damageColor,
            _damageEffectCurve.Evaluate(_vignetteTimer / _damageEffectTime));
            yield return null;
        }

        _vignetteTimer = 0;
        
        while (_vignetteTimer <= _damageEffectTime)
        {
            _vignetteTimer += Time.deltaTime;
            vignette.color.value = Color.Lerp(_damageColor,
            _vignetteStartColor,
            _damageEffectCurve.Evaluate(_vignetteTimer / _damageEffectTime));
            yield return null;
        }
        
        _vignetteTimer = 0;


    }

    #endregion

}
