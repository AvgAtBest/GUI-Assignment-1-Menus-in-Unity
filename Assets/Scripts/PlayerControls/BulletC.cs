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
        //adds a impulse force to the bullet rigidbody in the direction * speed
        bRigid.AddForce(direction * bSpeed, ForceMode.Impulse);
        //Destroys gameobject after 2 secs
        Destroy(gameObject,2);
    }
}
