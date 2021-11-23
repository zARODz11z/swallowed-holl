using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Lizbeth Solis
public class spikeDeath : MonoBehaviour
{
    PlayerStats ps;
    
    [SerializeField]
    public float damageSpikes = 1.0f;

    private void onCollisionEnter(Collision collision)
        {
            if(collision.gameObject.name == "Spikes")
            {
                //health -= damageSpikes;
                ps.takeDamage(damageSpikes);
            }
        }
}

