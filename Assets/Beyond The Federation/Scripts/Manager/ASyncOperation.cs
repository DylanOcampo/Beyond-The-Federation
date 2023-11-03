//This script lets you load a Scene asynchronously. It uses an asyncOperation to calculate the progress and outputs the current progress to Text (could also be used to make progress bars).

//Attach this script to a GameObject
//Create a Button (Create>UI>Button) and a Text GameObject (Create>UI>Text) and attach them both to the Inspector of your GameObject
//In Play Mode, press your Button to load the Scene, and the Text changes depending on progress. Press the space key to activate the Scene.
//Note: The progress may look like it goes straight to 100% if your Scene doesn’t have a lot to load.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ASyncOperation : MonoBehaviour
{

    AsyncOperation asyncOperation;

    public bool ShowMessage;

    void Start()
    {
        //Call the LoadButton() function when the user clicks this Button

    }

    public void StartLoadScene(string scene)
    {
        
        asyncOperation = SceneManager.LoadSceneAsync(scene);
        
        asyncOperation.allowSceneActivation = false;
    }

    public void StartLoadedScene()
    {
        asyncOperation.allowSceneActivation = true;
    }

    public float CheckStatus()
    {
        return asyncOperation.progress;
    }

    
}
