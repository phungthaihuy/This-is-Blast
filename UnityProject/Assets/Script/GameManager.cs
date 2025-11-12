using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Dictionary<BlockShooter, BulletsPool> shooterPools = new Dictionary<BlockShooter, BulletsPool>();

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
}
