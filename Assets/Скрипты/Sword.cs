using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 10;
    public static float time;
    public static bool attacked = false;
    public EnemyControl enemyControl;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        enemyControl = collision.GetComponent<EnemyControl>();
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        Animator animator = collision.GetComponent<Animator>();
        if (collision.gameObject.tag == "Enemy" && enemy.die == false )
        {
            enemy.swordAttackThisEnemy = true;
            attacked = true;
            enemy.health -= damage;
            if (enemyControl.enemyAttack == false)
            {
                //enemyControl.speed = 0;
                animator.SetBool("Attacked", true);
            }
            time = Time.time;
        }

    }

}
