using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Andrew Rodriguez with the help of https://www.youtube.com/watch?v=XOjd_qU2Ido&ab_channel=Brackeys
//This class is what is serialized into binary in our save system class
[System.Serializable]
public class PlayerData
{
    public float health;
    public float hunger;
    public float[] position;
    public bool hollOrReal;
    public PlayerData (PlayerStats playerStats)
    {
        health = playerStats.hp;
        hunger = playerStats.hunger;
        position = new float[3];
        position[0] = playerStats.transform.position.x;
        position[1] = playerStats.transform.position.y;
        position[2] = playerStats.transform.position.z;
        hollOrReal = playerStats.gameObject.GetComponent<WorldShift>().hollOrReal;
    }
}
