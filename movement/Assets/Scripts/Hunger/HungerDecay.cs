using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerDecay : MonoBehaviour
{
    private float interval;
    private PlayerStats ps;
    public float decayRate;


    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PlayerStats>();
        interval = 0;
    }

    public void changeHunger(int val)
    {
        ps.hunger += val;
        if (ps.hunger > 100)
            ps.hunger = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ps.hunger -= 10;
        }

        if (ps.hunger < 0)
            ps.hunger = 0; 

        if (interval > 1)
        {
            if (ps.hunger > 0)
                ps.hunger -= decayRate;
            else
            {
                ps.hp -= 1;
            }

            interval = 0;
        }
        interval += UnityEngine.Time.deltaTime;
    }
}
