using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BlockShooter : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] BulletsPool bulletPool;

    private float fireCountDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        FireRate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FireRate()
    {
        if (fireCountDown <= 0f)
        {
            fireCountDown = .1f;
            Shoot();
        }
        fireCountDown -= Time.deltaTime;
    }
    void Shoot()
    {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = firePoint.position;
    }
}
