using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenuSystem : MonoBehaviour
{
    private bool isShowing = false;
    public GameObject Menu;
    
    
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayScene(string value)
    {
        SceneManager.LoadScene(value);
    }

    public void PauseMenu()
    {
        Time.timeScale = 1;
        isShowing = false;
        Menu.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            Menu.SetActive(isShowing);
            if (isShowing)
            {
                Time.timeScale = 0;
            }else{
                Time.timeScale = 1;
            }
        }
    }

    
}
