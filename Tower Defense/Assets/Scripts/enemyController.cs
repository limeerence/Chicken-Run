using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Enemy))]
public class enemyController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private gameController controller;
    [SerializeField] private Enemy enemy;

    private GameObject prev;
    private GameObject next;
    private bool dead = false;

    private Rigidbody rbd;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rbd = gameObject.GetComponent<Rigidbody>();

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
        anim.SetBool(enemy.animSpd, true);

        if (Convert.ToInt32(prev.name) < pathGenerator.path.Count - 1)
        {
            next = pathGenerator.path[Convert.ToInt32(prev.name) + 1];
            transform.position += (next.transform.position - transform.position).normalized * enemy.speed * Time.deltaTime;
            Quaternion rot = Quaternion.LookRotation((next.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 1.8f);

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
        if (enemy.health <= 0 && !dead)
        {
            dead = true;
            controller.coins += enemy.coins;
            Destroy(gameObject);
        }
    }
}
