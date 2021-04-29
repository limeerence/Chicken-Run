using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Enemy))]
public class enemyController : MonoBehaviour
{
    [SerializeField] private gameController controller;
    [SerializeField] private Enemy enemy;

    private GameObject prev;
    private GameObject next;
    private bool dead = false;

    private void Start()
    {
        prev = pathGenerator.path[0];
        transform.position = prev.transform.position;

        if (!controller)
        {
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        }

        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (Convert.ToInt32(prev.name) < pathGenerator.path.Count - 1)
        {
            next = pathGenerator.path[Convert.ToInt32(prev.name) + 1];
            transform.position += (next.transform.position - transform.position).normalized * enemy.speed * Time.deltaTime;
            Quaternion rot = Quaternion.LookRotation((next.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 1.5f);

            if (Vector3.Distance(transform.position, next.transform.position) <= 0.01f)
                prev = next;
        } else
        {
            //damage player if reach end
            controller.updateHealth(-enemy.damage);
            Destroy(gameObject);
        }
    }

    public void takeDamage(int dmg)
    {
        if (!dead && enemy != null)
        {
            enemy.health -= dmg;
        }
        if (enemy.health <= 0)
        {
            dead = true;
            controller.coins += enemy.coins;
            Destroy(gameObject);
        }
    }
}
