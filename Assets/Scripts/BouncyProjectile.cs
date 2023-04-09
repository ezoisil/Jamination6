using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncyProjectile : Projectile
{
    [SerializeField] private float _rayDistance = .5f;
    [SerializeField] private LayerMask _targetLayerMask;

    private Vector3 _bounceDirection;
    private Vector3 _bouncePoint;


    private void OnTriggerEnter(Collider other)
    {
        if ((_targetLayerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            CalculateBounceDirection();
            transform.forward = _bounceDirection;
            transform.position = _bouncePoint;
        }

    }
    //
    // private IEnumerator MoveToTarget()
    // {
    //     CalculateBounceDirection();
    //     transform.position = 
    // }

    private void CalculateBounceDirection()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _rayDistance))
        {
            var xDir = new Vector3(0, 0, transform.forward.x);
            var flatNormal = new Vector3(hitInfo.normal.x, 0, hitInfo.normal.z);
            _bounceDirection = Vector3.Reflect(transform.forward, flatNormal);
            _bouncePoint = hitInfo.point;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * _rayDistance);
        Gizmos.color = Color.red;
        CalculateBounceDirection();
        Gizmos.DrawRay(transform.position, _bounceDirection);

    }
}
