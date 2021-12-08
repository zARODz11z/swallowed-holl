using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundTrackToggler : MonoBehaviour
{
    public void swapOff(){
        Debug.Log("SWAPPED");
        transform.GetChild(0).gameObject.GetComponent<AudioSource>().volume = .1f;
        transform.GetChild(1).gameObject.GetComponent<AudioSource>().volume = 0;
    }
    public void swapOn(){
        Debug.Log("SWAPPED");
        transform.GetChild(0).gameObject.GetComponent<AudioSource>().volume = 0;
        transform.GetChild(1).gameObject.GetComponent<AudioSource>().volume = .1f;
    }
}
