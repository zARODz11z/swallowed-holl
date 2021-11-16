using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float hunger;
    public float[] position;

    public PlayerData (PlayerStats playerStats)
    {
        health = playerStats.hp;
        hunger = playerStats.hunger;
        position = new float[3];
        position[0] = playerStats.transform.position.x;
        position[1] = playerStats.transform.position.y;
        position[2] = playerStats.transform.position.z;

    }
}
