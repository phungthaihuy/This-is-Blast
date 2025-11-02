using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float bulletSpeed = 10f;
    public void Seek (Transform _target)
    {
        target = _target;
    }
    private void Update()
    {
        if (target == null) return;

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        BlockShooter[] shooters = FindObjectsOfType<BlockShooter>();
        foreach (BlockShooter shooter in shooters)
        {
            if (shooter != null)
                shooter.RemoveCube(target.gameObject);
        }
       

        Destroy(target.gameObject);
        FindObjectOfType<BulletsPool>().ReturnBullet(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(target.gameObject);
    }
}
