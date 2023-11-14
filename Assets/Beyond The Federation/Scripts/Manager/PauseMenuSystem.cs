using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using static UnityEditor.Experimental.GraphView.GraphView;
using DG.Tweening;

public class PauseMenuSystem : MonoBehaviour
{
    private bool isShowing = false;
    public GameObject Menu, ActualBackground, MainMenu_Menu, Inventory_Menu;
    public Animator MenuAnimator;

    public bool isThisMainMenu;
    
    public KeyCode PauseKey = KeyCode.Escape;
    public string NameOfGameScene;

    bool isInventory = false;


    private void Start()
    {
        if (isThisMainMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameObject.GetComponent<ASyncOperation>().StartLoadScene(NameOfGameScene);
        }
        else
        {
            MainMenu_Menu.GetComponent<CanvasGroup>().alpha = 0f;
            Inventory_Menu.GetComponent<CanvasGroup>().alpha = 0f;
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
        if (isInventory)
        {
            Inventory_Menu.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetUpdate(true).OnComplete(() =>
            {
                ActualBackground.SetActive(false);
                MenuAnimator.SetTrigger("Next");
            });
        }
        else
        {
            MainMenu_Menu.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetUpdate(true).OnComplete(() =>
            {
                ActualBackground.SetActive(false);
                MenuAnimator.SetTrigger("Next");
            });
        }
        
        
        
    }

    public void NextMenu_AnimationCallback()
    {
        if (isInventory)
        {
            ActualBackground.SetActive(true);
            MainMenu_Menu.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetUpdate(true);
        }
        else
        {
            ActualBackground.SetActive(true);
            Inventory_Menu.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetUpdate(true);
        }
        isInventory = !isInventory;
    }


    public void StartIdle()
    {
        ActualBackground.SetActive(true);
        MainMenu_Menu.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetUpdate(true);
        
    }

    public void Resume()
    {
        isShowing = false;
        Menu.SetActive(isShowing);
        ActualBackground.SetActive(false);
        MainMenu_Menu.GetComponent<CanvasGroup>().alpha = 0f;
        Inventory_Menu.GetComponent<CanvasGroup>().alpha = 0f;
        Time.timeScale = 1;
    }



    void Update()
    {
        if (Input.GetKeyDown(PauseKey) && !isThisMainMenu)
        {
            


            isShowing = !isShowing;
            Menu.SetActive(isShowing);
            if (isShowing)
            {
                MenuAnimator.speed = 1;
                Time.timeScale = 0;
            }else{
                ActualBackground.SetActive(false);
                MainMenu_Menu.GetComponent<CanvasGroup>().alpha = 0f;
                Inventory_Menu.GetComponent<CanvasGroup>().alpha = 0f;
                Time.timeScale = 1;
               
            }
        }
    }

   

}
