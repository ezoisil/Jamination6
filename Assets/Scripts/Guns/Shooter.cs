using System;
using UnityEngine;

namespace Guns
{

    public class Shooter : CustomUpdateBehaviour
    {
        [SerializeField] private float _frequency = 1;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private bool _isShooting = true;
        [SerializeField] private Transform _instantiateAt;

        private float _timer;

        public override void OnCustomUpdate(float deltaTime)
        {
            base.OnCustomUpdate(deltaTime);
            if(!_isShooting) return;
            _timer += Time.deltaTime;
            if(_timer<_frequency) return;
            
            Shoot();
            _timer = 0;
        }


        private void Shoot()
        {
            var projectile = Instantiate(_projectilePrefab, transform);
            projectile.transform.forward = transform.forward;
            projectile.transform.position = _instantiateAt.position;
        }



    }

}
