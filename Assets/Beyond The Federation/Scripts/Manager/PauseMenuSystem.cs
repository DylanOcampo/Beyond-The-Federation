using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PauseMenuSystem : MonoBehaviour
{
    private bool isShowing = false;
    public GameObject Menu, ActualBackground;
    public Animator MenuAnimator;

    public bool isThisMainMenu;
    
    public KeyCode PauseKey = KeyCode.Escape;
    public string NameOfGameScene;


    private void Start()
    {
        if (isThisMainMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameObject.GetComponent<ASyncOperation>().StartLoadScene(NameOfGameScene);
        }
    }

    public void SoundEnter()
    {
        AudioManager.instance.PlayClip(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void ReloadGameScene()
    {
        SceneManager.LoadScene(NameOfGameScene);
    }

    public void FirstScene()
    {
        gameObject.GetComponent<ASyncOperation>().StartLoadedScene();
    }

    public void NextMenu()
    {
        MenuAnimator.SetTrigger("Next");
        
    }



    void Update()
    {
        if (Input.GetKeyDown(PauseKey) && !isThisMainMenu)
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
