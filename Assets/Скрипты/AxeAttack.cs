using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttack : MonoBehaviour
{
    public int damage = 10;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        ����� hero = go.GetComponent<�����>();
        ������������ roll = go.GetComponent<������������>();
        if (go.tag =="Player" && roll.roll == false)
        {
            hero.health -= damage;
        }
    }
}
