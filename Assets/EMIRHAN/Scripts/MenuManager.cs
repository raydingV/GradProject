using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void BeginPlay()
    {
        SceneManager.LoadScene("Assets/EMIRHAN/SCENES/EnteranceImplement.unity");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
