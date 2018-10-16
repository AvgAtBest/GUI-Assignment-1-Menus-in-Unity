using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletC : MonoBehaviour
{
    public Rigidbody bRigid;
    public GameObject spawnPoint;
    public float bSpeed = 20f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Fire(Vector3 direction)
    {
        bRigid.AddForce(direction * bSpeed, ForceMode.Impulse);
        Destroy(gameObject,2);
    }
}
