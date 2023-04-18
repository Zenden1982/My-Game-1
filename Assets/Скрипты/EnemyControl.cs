using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform player; // ссылка на объект игрока
    public float speed = 3f; // скорость движения врага
    public bool flipRight = true;
    public bool walk = false;
    public Animator animator;
    public Enemy enemy;
    public float checkGroundOffSetY = 0f;
    public float checkGroundRadius = 1f;
    public float checkPlayerRadius = 2f;
    public bool enemyAttack = false;
    public GameObject axe;
    private float saveSpeed;
    private Rigidbody2D rb;
    private Герой hero;

    private void Start()
    {
        hero = GameObject.Find("Knight").GetComponent<Герой>();
        saveSpeed = speed;
        enemy = this.GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        axe = transform.Find("Axe").gameObject;
        axe.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!enemy.die && !enemyAttack && !hero.playerIsDead)
        {
            walk = true;
            animator.SetBool("Walk", walk);
            Vector2 direction = (Vector2)player.position - rb.position; // вычисляем направление к игроку
            direction.Normalize(); // нормализуем вектор направления, чтобы скорость была постоянной
            if (player.position.x - rb.position.x > 0 && !flipRight)
            {
                Flip();
            }
            else if (player.position.x - rb.position.x < 0 && flipRight)
            {
                Flip();
            }
            Vector2 xDirection = Vector2.zero; // Останавливаем
            xDirection.x = direction.x;
            rb.velocity = xDirection * speed; // устанавливаем скорость движения врага


        }
        else
        {
            walk = false;
            animator.SetBool("Walk", walk);
        }
        //Анимация Атаки
        if (PlayerCheckerAttack())
        {
            enemyAttack = true;
            animator.SetBool("EnemyAttack", enemyAttack);
        }


    }

    void Flip()
    {
        flipRight = !flipRight;
        Vector3 flipPos = transform.localScale;
        flipPos.x *= -1;
        transform.localScale = flipPos;
    }

    public void Die()
    {
        animator.SetBool("Die", false);
        Destroy(this.gameObject);

    }

    public bool PlayerCheckerAttack()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + checkGroundOffSetY), checkGroundRadius);
        foreach (var coleder2D in collider)
        {
            GameObject go = coleder2D.gameObject;
            if (go.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    public void OffAttack()
    {
        animator.SetBool("EnemyAttack", false);
        enemyAttack = false;
    }

    public void ColliderOn()
    {
        axe.SetActive(true);
    }

    public void ColliderOff()
    {
        axe.SetActive(false);
    }

    public void StopWalk()
    {
        rb.velocity=Vector2.zero;
        speed = 0;
    }

    public void ResumeWalk()
    {
        speed = saveSpeed;
    }

    //public bool PlayerChecker()
    //{
    //    Collider2D[] collider = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + checkGroundOffSetY), checkPlayerRadius);
    //    foreach(var collider2D in collider)
    //    {
    //        if (collider2D.tag == "Player")
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

}
