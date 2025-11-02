using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BlockShooter : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] BulletsPool bulletPool;

    private const string CUBE = "Cube";

    private GameObject[] cubes;
    private GameObject nearestCube = null;
    private Transform target;
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
        cubes = GameObject.FindGameObjectsWithTag(CUBE);
        float shortedDistance = Mathf.Infinity;
        foreach (GameObject cube in cubes)
        {
            float distanceCube = Vector3.Distance(transform.position, cube.transform.position);
            if (distanceCube < shortedDistance)
            {
                shortedDistance = distanceCube;
                nearestCube = cube;
            }
        }
        if (nearestCube != null)
        {
            target = nearestCube.transform;
        }
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = firePoint.position;
    }
}
