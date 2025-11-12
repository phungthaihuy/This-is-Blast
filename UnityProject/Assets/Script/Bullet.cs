using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private BlockShooter shooter;
    private float bulletSpeed = 10f;
    public void Seek (Transform _target, BlockShooter _shooter)
    {
        target = _target;
        shooter = _shooter;
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
        target = null;
        //FindObjectOfType<BulletsPool>().ReturnBullet(gameObject);
        ReturnToPool();
    }
    private void ReturnToPool()
    {
        if (shooter != null)
        {
            BulletsPool pool = GameManager.Instance.GetPoolForShooter(shooter); //Get Suitable Pool
            {
                pool.ReturnBullet(gameObject);
                return;
            }
        }
        //Destroy(gameObject); 
    }
}
