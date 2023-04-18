using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    public int health;
    public int maxHealth = 100;
    public Animator animator;
    public Collider2D col;
    public bool die = false;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public bool isBlinking = false;
    public bool swordAttackThisEnemy = false;
    public TextMeshProUGUI textScore;
    public GameObject heal;
    public GameObject amogus;
    public GameObject win;
    public void Start()
    {


        //amogus.SetActive(false);
        textScore = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        sprite = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<Collider2D>();
        animator = this.GetComponent<Animator>();
        GameObject game = GetComponent<GameObject>();
        health = maxHealth;
    }

    public void Update()
    {
        
        
        if (Sword.attacked == false)
        {
            Sword.attacked = false;
            animator.SetBool("Attacked", false);
        }
        if (health <= 0 && die == false)
        {


            DeathEnemy();


        }

        if (swordAttackThisEnemy && !isBlinking)
        {
            StartCoroutine(BlinkCoroutine());
        }



    }

    private IEnumerator BlinkCoroutine()
    {
        isBlinking = true;
        Color originalColor = sprite.color;
        Color blinkColor = Color.red;
        float blinkDuration = 0.1f;
        int blinkCount = 1;

        for (int i = 0; i < blinkCount; i++)
        {
            sprite.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            sprite.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }

        isBlinking = false;
        swordAttackThisEnemy = false;
    }
    
    public void ScoreAdd() // Добавляет очки после анимации смерти
    {
        CameraController.score++;
        textScore.text = "Killed: " + CameraController.score.ToString();
    }

    public void DeathEnemy()
    {
        EnemyControl enemyControl = this.GetComponent<EnemyControl>();
        enemyControl.enabled = false;
        rb.velocity = Vector2.zero; // Выключаем всё чтобы враг не двигался
        rb.isKinematic = true;
        col.isTrigger = true;

        animator.SetBool("Die", true);
        die = true;
    }

    public void HealSpawner()
    {
        if (heal !=null)
        {
            GameObject heart = Instantiate(heal);
            heart.transform.position = transform.position;
        }
    }


}
