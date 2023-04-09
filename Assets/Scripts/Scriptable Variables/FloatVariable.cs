using System;
using UnityEngine;

namespace Core.Scriptable_Variables
{

    [CreateAssetMenu(menuName = "Float Variable")]
    public class FloatVariable : ScriptableObject
    {
        [SerializeField] private float _value;
        public delegate void ValueChangeHandler(float changeAmount);
        public event ValueChangeHandler OnValueChanged;

        public float GetValue()
        {
            return _value;
        }

        public void ChangeValue(float value)
        {
            float oldValue = _value;
            _value = value;
            OnValueChanged?.Invoke(value - oldValue);
        }
        
        
        
    }

}
