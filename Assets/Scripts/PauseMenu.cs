using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isActive = false;

    [SerializeField] GameObject pauseMenu;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameManager.GUI.gameObject.SetActive(false);
    }

    public void Resume ()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameManager.GUI.gameObject.SetActive(true);

    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void Toggle()
    {
        if (isActive)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
