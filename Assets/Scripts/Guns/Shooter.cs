using System;
using System.Collections.Generic;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            DrawRay(transform.position, transform.forward);
            
            List<Vector3> hitPoints = new List<Vector3>();
            hitPoints.Add(transform.position);
            Vector3 nextDir = transform.forward;
            for (int i = 0; i <= 14; i++)
            {
                Ray ray = new Ray(hitPoints[i], nextDir );
                // Gizmos.DrawRay(hitPoints[i], nextDir*10);
                if (Physics.Raycast(hitPoints[i],nextDir, out RaycastHit hit))
                {
                    hitPoints.Add(hit.point);
                    nextDir = Vector3.Reflect(nextDir, hit.normal);
                }
            }
            for (int i = 0; i < 14; i++)
            {
                 Gizmos.DrawLine(hitPoints[i],hitPoints[i+1]);
            }
        }

        private void DrawRay(Vector3 point, Vector3 dir)
        {
          
     
        }


    }

}
