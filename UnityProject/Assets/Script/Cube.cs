using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    
    [SerializeField] Rigidbody rb;

    private List<BlockShooter> shooters;
    private bool isTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        shooters = new List<BlockShooter>(FindObjectsOfType<BlockShooter>());
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0f, 0f, -1f), ForceMode.Force);
        foreach (BlockShooter shooter in shooters)
        {
            if (shooter.GetTarget() == transform.parent) isTarget = true;
        }
    }

    public bool GetIsTarget()
    {
        return isTarget;
    }

    public void SetIsTarget (bool _isTarget)
    {
        isTarget = _isTarget;
    }
}
