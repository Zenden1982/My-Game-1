using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Герой : MonoBehaviour
{
    public int health;
    public int maxHealth=100;
    public Image image;
    public float fill;
    public bool playerIsDead = false;
    public Animator animator;
    public Rigidbody2D rb;
    public Collider2D col;
    public Image blackImage;
    public Color colorImage;
    public TextMeshProUGUI text;
    public float fadeInTime = 2f;
    public float timer = 0f;
    private float alpha;
    public void Start()
    {
        text = GameObject.Find("Death").GetComponent<TextMeshProUGUI>();
        text.enabled = false;
        blackImage = GameObject.Find("BlackGround").GetComponent<Image>();
        colorImage = blackImage.color;
        colorImage.a = 0;
        blackImage.color = colorImage;
        rb = this.GetComponent<Rigidbody2D>();
        col= GetComponent<Collider2D>();
        health = maxHealth;
        animator = GetComponent<Animator>();
        
    }
    public void Update()
    {
        if (YouWin.winBool)
        {
            if (Input.GetKey(KeyCode.R))
            {
                CameraController.score= 0;
                playerIsDead = false;
                SceneManager.LoadScene("SampleScene");

            }
        }
            fill = health;
        image.fillAmount = fill/100;

        if (health <= 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                CameraController.score= 0;
                playerIsDead = false;
                SceneManager.LoadScene("SampleScene");
                
            }
            if (timer < fadeInTime && alpha < 0.5)
            {
                timer += Time.deltaTime;
                alpha = timer / fadeInTime; // устанавливаем альфа канал в соответствии с прошедшим временем
                colorImage.a = alpha;
                blackImage.color = colorImage;
            }
            
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            col.enabled = false;
            playerIsDead = true;
            animator.SetBool("PlayerDead", playerIsDead);
        }
    }


    void TextEnable()
    {
        text.enabled=true;
    }

}
