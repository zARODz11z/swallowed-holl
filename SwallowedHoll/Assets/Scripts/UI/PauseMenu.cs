using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Brian Meginness
public class PauseMenu : MonoBehaviour {
    // Components for each UI screen
    GameObject pauseUI;
    GameObject settingUI;
    GameObject helpUI;

    //Flipflop bool to pause and unpause with the same button
   public bool isPaused = false;


    // Start is called before the first frame update
    void Start() {
        //Get components, make sure the game can't start paused
        pauseUI = gameObject.transform.Find("Top").gameObject;
        pauseUI.SetActive(false);
        settingUI = gameObject.transform.Find("Settings").gameObject;
        settingUI.SetActive(false);
        helpUI = gameObject.transform.Find("Help").gameObject;
        helpUI.SetActive(false);
    }

    private void Update(){
        //IF pause key is pressed
        if (Input.GetKeyDown(KeyCode.Escape)){
            //IF not paused, pause
            if (!isPaused){
                pause();
            }
            //IF paused, resume
            else{
                resume();
            }
        }
    }

    //Pause the game by freezing time and enabling the menu
    void pause(){
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //Unfreeze time, hide the menu
    public void resume(){
        Debug.Log("resume");
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Show settings menu
    public void settings(){
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
    }

    //Show help menu
    public void help(){
        pauseUI.SetActive(false);
        helpUI.SetActive(true);
    }

    //Return to top level pause menu
    public void back(){
        pauseUI.SetActive(true);
        helpUI.SetActive(false);
        settingUI.SetActive(false);
    }

    //Return to main menu
    public void mainMenu(){
        SceneManager.LoadScene(0);
    }

    //Quit game
    public void quit(){
        Debug.Log("Quitting game from pause");
        Application.Quit();
    }
}
