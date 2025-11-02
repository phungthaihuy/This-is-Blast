using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BlockShooter : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] BulletsPool bulletPool;

    private const string CUBE = "Cube";

    public List<GameObject> cubes;
    private GameObject nearestCube = null;
    private Transform target;
    private float fireCountDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cubes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cube"));
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

            GameObject bulletGO = bulletPool.GetBullet();
            bulletGO.transform.position = firePoint.position;
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Seek(target);
        }
        
    }
    public void RemoveCube(GameObject cube)
    {
        cubes.Remove(cube);
    }
}
