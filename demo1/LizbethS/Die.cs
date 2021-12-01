using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Lizbeth
// This method causes the object to disappear
public class Die : MonoBehaviour
{
    [SerializeField]  //This allows the player to change the variable on Unity
    float lifeMin = 5;     // Creates float variable lifeMin and is set to 5
    [SerializeField]  //This allows the player to change the variable on Unity
    float lifeMax = 10;    // Creates float variable lifeMax and is set to 10

    // This function is called before the first frame update
    void Start()
    {
        // This randomly selects a float number from the range 5-10 as the time
        // it will take them to disappear 
        Destroy(this.gameObject, Random.Range(lifeMin, lifeMax));
    }
}
