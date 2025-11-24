using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BlockShooter : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] BulletsPool bulletPool;
    [SerializeField] Renderer ren;
    [SerializeField] List<GameObject> shootPosition;
    [SerializeField] List<GameObject> blocksShooter;

    private const string CUBE = "Cube";

    private List<GameObject> cubes;
    private GameObject nearestCube = null;
    private GameObject selectedObject;
    private Transform target;
    private Transform previousTarget;

    public int bullets;
    private float moveSpeed = 20f;
    private float rotationSpeed = 10f;
    private float fireCountDown = 0f;
    private int shootOrder = 0;
    private bool getIsTargetCube;


    private void Awake()
    {
        if (GameManager.Instance != null && bulletPool != null)
            GameManager.Instance.RegisterShooterPool(this, bulletPool);
    }
    void Start()
    {
        cubes = GameManager.Instance.cubes;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("shootOrder: " + shootOrder);
        MoveThenShoot();
        
        foreach (GameObject item in shootPosition)
        {
            if (gameObject.transform.parent == item.gameObject.transform)
            {
                if (bullets > 0) FireRate();
            }
        }
        
    }
    private void MoveThenShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.transform.gameObject;
                if (clickedObject == gameObject)
                {
                    selectedObject = gameObject;
                }
            }
        }
        if (selectedObject != null)
        {
            foreach (GameObject shooter in blocksShooter)
            {
                if (shootOrder == shootPosition.Count)
                {
                    shootOrder = 0;
                    return;
                }
                if (shooter.transform.IsChildOf(shootPosition[shootOrder].transform)) shootOrder++;
            }
            selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position,
                new Vector3(shootPosition[shootOrder].transform.position.x, selectedObject.transform.position.y, shootPosition[shootOrder].transform.position.z),
                moveSpeed * Time.deltaTime);
            if (Vector3.Distance(selectedObject.transform.position,
                new Vector3(shootPosition[shootOrder].transform.position.x, selectedObject.transform.position.y, shootPosition[shootOrder].transform.position.z)) < 0.01f)
            {
                selectedObject.transform.parent = null;
                selectedObject.transform.SetParent(shootPosition[shootOrder].transform);
                selectedObject = null;
            }
        }
    }
    void UpdateTarget()
    {
        if (cubes.Count == 0)
        {
            target = null;
            previousTarget = null;
            nearestCube = null;
        }
        float shortedDistance = Mathf.Infinity;
        foreach (GameObject cube in cubes)
        {
            getIsTargetCube = cube.transform.GetChild(0).GetComponent<Cube>().GetIsTarget();
            if (getIsTargetCube == true) continue;
            if (cube.transform.GetChild(0).GetComponent<Renderer>().material.name == ren.material.name)
            {
                float distanceCube = Vector3.Distance(transform.position, cube.transform.position);
                if (distanceCube < shortedDistance)
                {
                    shortedDistance = distanceCube;
                    nearestCube = cube;
                }
            }
        }
        if (nearestCube != null)
        {
            target = nearestCube.transform;
            target.GetChild(0).GetComponent<Cube>().SetIsTarget(true);
        }
    }
    private void FireRate()
    {
        if (fireCountDown <= 0f)
        {
            fireCountDown = .2f;
            UpdateTarget();
            if (target == null) return;
            ShooterRotation();
            if (target == previousTarget) return;
            Shoot();
        }
        fireCountDown -= Time.deltaTime;
    }
    void Shoot()
    {
        GameObject bulletGO = bulletPool.GetBullet();
        bulletGO.transform.position = firePoint.position;
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target, this);
        bullets--;
        previousTarget = target;
        target = null;
    }

    private void ShooterRotation()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void RemoveCube(GameObject cube)
    {
        cubes.Remove(cube);
    }

    public Transform GetTarget()
    {
        return target;
    }
}
