using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingHeart : MonoBehaviour
{
    private float speed = 0.8f;
    private void Update()
    {
        Vector2 postion = transform.position;
        if (postion.y <= -17f)
        {
            speed = 0.8f;
        }
        else if (postion.y >= -16.5f)
        {
            speed = -0.8f;
        }
        postion.y +=speed*Time.deltaTime;
        transform.position = postion;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Герой hero = collision.gameObject.GetComponent<Герой>();
            hero.health +=20;
            Destroy(this.gameObject);
        }
    }
}
