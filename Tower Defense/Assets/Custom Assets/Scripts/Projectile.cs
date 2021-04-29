using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float lifespan = 1.5f;
    private bool collided = false;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Enemy" && !collided)
        {
            collided = true;
            Destroy(gameObject);
        }
    }

}
