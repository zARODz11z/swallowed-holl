using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCaller : MonoBehaviour
{
    [SerializeField]
    GameObject[] punches;
    int index;
    public void playPunch(){   
        index = Random.Range(0, punches.Length);
        punches[index].GetComponent<AudioSource>().Play();
    }
}
