using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eatingSounds : MonoBehaviour
{
    [SerializeField] private AudioSource[] eatingAudio;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
                int index = Random.Range(0, eatingAudio.Length);
                Debug.Log(index);
                eatingAudio[index].Play();
            
        }
    }
}
