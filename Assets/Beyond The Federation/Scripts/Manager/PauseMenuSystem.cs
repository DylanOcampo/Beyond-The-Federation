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
        SceneManager.LoadScene("MainMenu");
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
