using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is just so that we can disable the dynamic bone, or "flopyness" of the hands at will
//Author: Lizbeth
public class disableDynamicBone : MonoBehaviour
{
    [SerializeField]     //This allows the player to change the variable on Unity
    GameObject boneslot1;
    [SerializeField]     //This allows the player to change the variable on Unity
    GameObject boneslot2;
    // Start is called before the first frame update

    // This function enables/ disables the hands'dynamic bones depending on the
    //  boolean value of plug
    public void toggle(bool plug){
        if (plug){  //if plug is true, dynamic bones are disabled
            boneslot1.GetComponent<DynamicBone>().enabled = false;
            boneslot2.GetComponent<DynamicBone>().enabled = false;
        }
        else if (!plug){    //if plug is false, dynamic bones are enabled
            boneslot1.GetComponent<DynamicBone>().enabled = true;
            boneslot2.GetComponent<DynamicBone>().enabled = true;
        }
    }
}