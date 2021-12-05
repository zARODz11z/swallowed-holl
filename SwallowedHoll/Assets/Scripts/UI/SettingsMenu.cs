using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Brian Meginness
public class SettingsMenu : MonoBehaviour
{
    //Menu components
    Slider volSlide;

    Controls controls;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get components
        volSlide = GameObject.Find("VolumeSlide").GetComponent<Slider>();
        controls = GameObject.Find("Player").GetComponentInChildren<Controls>();
    }

    //On volume slider change
    public void changeVol()
    {
        //Slider OnChange() is called when initialized, sometimes before start() can finish
        if (volSlide)
        {
            Debug.Log("Vol Changed: " + volSlide.value);
        }
        
    }
}
