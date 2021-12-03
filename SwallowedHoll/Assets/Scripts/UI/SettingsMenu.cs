using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Slider volSlide;
    Slider brightSlide;
    // Start is called before the first frame update
    void Start()
    {
        volSlide = GameObject.Find("VolumeSlide").GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeVol()
    {
        if (volSlide)
        {
            Debug.Log("Vol Changed: " + volSlide.value);
        }
        
    }

}
