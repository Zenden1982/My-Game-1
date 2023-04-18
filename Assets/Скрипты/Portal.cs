using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //SceneManager.LoadScene("SampleScene");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("SampleScene");
    }

}
