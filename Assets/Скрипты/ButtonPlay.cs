using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Escape()
    {
        Application.Quit();
    }
}
