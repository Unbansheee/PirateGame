using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;

    public void Play(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        Debug.Log("Play");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
