using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

}
