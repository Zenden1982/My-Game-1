using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Передвижение : MonoBehaviour
{
    public float speed = 1f;
    public float jump = 2f;
    public bool flipRight = true;
    public float checkGroundOffSetY = -1.8f;
    public float checkGroundRadius = 0.3f;
    public static bool isJumping;
    public Rigidbody2D rb;
    public Animator animator;
    public bool run = false;
    public bool roll =false;
    static public bool attack = false;
    public float time;
    public GameObject sword;
    public int enemyLayer;
    public Enemy[] enemy;
    public Герой hero;
    public void Start()
    {
        hero = GetComponent<Герой>();
        enemy = GetComponents<Enemy>();
        enemyLayer = LayerMask.NameToLayer("Enemy");
        sword = GameObject.Find("Sword");
        sword.SetActive(false);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!hero.playerIsDead)
        {
            isJumping = !IsGround();
            Vector2 pos = transform.position;
            float xAxis = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(xAxis * speed, rb.velocity.y);

            if (xAxis != 0f)
            {
                run = true;
            }
            else run = false;
            if (xAxis < 0 && flipRight)
            {
                Flip();

            }

            else if (xAxis > 0 && !flipRight)
            {
                Flip();
            }
            animator.SetBool("Run", run);

            //if (Input.GetKey(KeyCode.Space) && IsGround())
            //{

            //    rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
            //}

            Roll();

            //Анимация прыжка
            if (isJumping && roll == false)
            {
                animator.SetBool("Jumping", true);
            }
            else
            {
                animator.SetBool("Jumping", false);
            }
            

            transform.position = pos;

            //Атака
            //
            if (Input.GetMouseButtonUp(0) && !isJumping && !roll)
            {
                sword.SetActive(true);
                attack = true;
                animator.SetBool("Attack", true);
                time = Time.time;

            }
            else if (Time.time - time >= 0.02)
            {
                Sword.attacked = false;
                sword.SetActive(false);
                attack = false;
                animator.SetBool("Attack", false);
            }
        }
    }

    void Flip()
    {
        flipRight = !flipRight;
        Vector3 flipPos = transform.localScale;
        flipPos.x *= -1;
        transform.localScale = flipPos;
    }

    public bool IsGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + checkGroundOffSetY), checkGroundRadius);
        foreach (var coleder2D in collider)
        {
            GameObject go = coleder2D.gameObject;
            if (go.tag == "Ground")
            {
                return true;
            }
        }
        return false;

    }

    //Перекат
    public void Roll()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isJumping)
        {
            roll = true;
            animator.SetBool("Roll", roll);
            
        }
        else
        {
            animator.SetBool("Roll", false);
            
        }
    }

    public void CollisionIgnored()
    {
        if (enemy != null)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer);
        }
    }

    public void CollisionNotIngnored()
    {
        
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);
        
    }

    public void RollOff()
    {
        roll = false;
    }
}
