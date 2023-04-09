using Unity.VisualScripting;
using UnityEngine;

    public class Projectile : CustomUpdateBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private int _hitCount;
        [Header("Movement")]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private AnimationCurve _accelerationCurve;
        [SerializeField] private float _accelerationTime = 0;

        private float _timer;
        private void Move(float deltaTime)
        {
            _timer += deltaTime;
            float speed = _accelerationCurve.Evaluate(_timer / _accelerationTime) * _maxSpeed;
            var pos = transform.position;
            transform.position = pos + transform.forward * speed;
        }

        public override void OnCustomUpdate(float deltaTime)
        {
            base.OnCustomUpdate(deltaTime);
            Move(deltaTime);
        }
    }


