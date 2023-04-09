using Unity.VisualScripting;
using UnityEngine;

    public class Projectile : CustomUpdateBehaviour
    {
        [SerializeField] protected int _hitCount;
        [Header("Movement")]
        [SerializeField] protected float _maxSpeed;
        [SerializeField] protected AnimationCurve _accelerationCurve;
        [SerializeField] protected float _accelerationTime = 0;

        protected float _timer;
        protected virtual void Move(float deltaTime)
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


