using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Brian Meginness
public class MainMenu : MonoBehaviour
{
    //Menu components
    GameObject mainMenu;
    GameObject settingsMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get menu components
        mainMenu = gameObject.transform.Find("Main").gameObject;
        settingsMenu = gameObject.transform.Find("Settings").gameObject;

        //Make sure it starts on the main menu
        back();
    }

    //Start a new game
    /*This will probably be expanded if we incorporate 
     * difficulty settings and naming save files */
    public void newGame()
    {
        SceneManager.LoadScene(1);
    }

    //Load a saved game
    /* This is a placeholder, our save system is not
     * finished enough for this to be functional yet */
    public void loadGame()
    {
        Debug.Log("Dummy until save system is up and running");
    }

    //Quits the game
    public void quitGame()
    {
        Debug.Log("Game quitting from menu");
        Application.Quit();
    }

    //Show settings menu
    public void settings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    //Returns to main menu
    public void back()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
