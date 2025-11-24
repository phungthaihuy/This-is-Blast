using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static float progressBarImagesUINormalized;

    private const string CUBE = "Cube";
    private Dictionary<BlockShooter, BulletsPool> shooterPools = new Dictionary<BlockShooter, BulletsPool>();
    public List<GameObject> cubes;

    private int totalCubes;
    private int cubesNormalized;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        cubes = new List<GameObject>(GameObject.FindGameObjectsWithTag(CUBE));
        totalCubes = cubes.Count;
    }
    private void Update()
    {
        cubesNormalized = cubes.Count;
        progressBarImagesUINormalized = (float)cubesNormalized / totalCubes;
        Debug.Log(progressBarImagesUINormalized);
    }
    // Register: Call Shooter at Awake
    public void RegisterShooterPool(BlockShooter shooter, BulletsPool pool)
    {
        if (!shooterPools.ContainsKey(shooter))
        {
            shooterPools[shooter] = pool;
        }
    }

    // Get Suitable Pool for each Shooter
    public BulletsPool GetPoolForShooter(BlockShooter shooter)
    {
        if (shooterPools.TryGetValue(shooter, out BulletsPool pool))
        {
            return pool;
        }
        return null;
    }

    public float GetProgressBarImagesUINormalized()
    {
        return progressBarImagesUINormalized;
    }
}
