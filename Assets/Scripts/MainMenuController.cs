using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        //settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        //settingsMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        //settingsMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SessionScene"); 
    }

    public void QuitGame()
    {
        // Application.Quit() is not supported in the editor, so we use UnityEditor.EditorApplication.isPlaying = false to stop play mode in the editor.
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        
    }
}
