using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class towerShooting : MonoBehaviour
{
    private Tower tower;
    private GameObject target;
    private float nextShootTime = 0;
    private GameObject bullet;
    private float bulletSpeed = 10f;

    private void Start()
    {
        tower = GetComponent<Tower>();
        InvokeRepeating("selectTarget", 0f, 0.5f);
    }

    private void Update()
    {
        selectTarget();
        if (target == null)
        {
            return;
        }
        lockOnTarget();

        if (Time.time > nextShootTime)
        {
            shoot();
            nextShootTime = Time.time + (1 / tower.shootRate);
        }
    }

    private void selectTarget()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        int selectedEnemy = -1;
        float minDist = Mathf.Infinity;

        for (int i = 0; i < allEnemies.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, allEnemies[i].transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                selectedEnemy = i;
            }
        }

        if (selectedEnemy != -1 && minDist <= tower.towerRange)
        {
            target = allEnemies[selectedEnemy];
        } else
        {
            target = null;
        }
    }

    private void shoot()
    {
        //instantiate bullet
        bullet = Instantiate(tower.projectile, tower.towerHead.transform.position, tower.towerHead.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (target.transform.position - tower.towerHead.transform.position).normalized * bulletSpeed;
        //damage target
        if (target != null)
        {
            target.GetComponent<enemyController>().takeDamage(tower.towerDamage);
        }
    }

    private void lockOnTarget()
    {
        Vector3 dir = target.transform.position - tower.towerHead.transform.position;
        tower.towerHead.transform.rotation = Quaternion.LookRotation(dir);
        
    }
}
