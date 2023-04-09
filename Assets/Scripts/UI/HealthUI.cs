using System;
using System.Collections;
using System.Collections.Generic;
using Core.Scriptable_Variables;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthPointPrefab;
    [SerializeField] private FloatVariable _playerHealth;

    private List<Image> _healthPointObjects= new List<Image>();

    private void Start()
    {
        _playerHealth.OnValueChanged += UpdateUI;
        Initialize();
    }

    private void OnDestroy()
    {
        _playerHealth.OnValueChanged -= UpdateUI;
    }

    private void UpdateUI(float changeAmount)
    {
        ClearPoints();
        InstantiatePoints();
    }

    private void Initialize()
    {
        InstantiatePoints();
    }

    private void ClearPoints()
    {
        for (int i = 0; i < _healthPointObjects.Count; i++)
        {
            var target = _healthPointObjects[i];
            Destroy(target.gameObject);
        }
        _healthPointObjects.Clear();
    }

    private void InstantiatePoints()
    {
        for (int i = 0; i < _playerHealth.GetValue(); i++)
        {
            var image =Instantiate(_healthPointPrefab, transform);
            _healthPointObjects.Add(image);
        }
    }

}
