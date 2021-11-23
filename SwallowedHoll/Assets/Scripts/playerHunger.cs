using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Brian Meginness and Travis Parks
//this script handles the players hunger and the rate at which it decays, assuming it decays at all. It also controls how it is increased. 
public class playerHunger : MonoBehaviour
{
    PlayerStats stats;
    float maxHunger = 100f;
    [SerializeField]
    float hungerRate;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    //Increases player's hunger by specified value
    public void increaseHunger(float food){
        //IF increase would put player at or over the max value
        if (stats.hunger + food >= maxHunger){
            //Set to max value
            stats.hunger = maxHunger;
        }
        else{
            //ELSE add value to current
            stats.hunger += food;
        }
    }

    //Old method, may be useful in the future
    //Decrease player hunger on a set interval
    // Update is called once per frame
   // void Update()
    //{
        //Debug.Log(stats.hunger);
        //if (stats.hunger <= 0){
       //     stats.hunger = 0;
           // Debug.Log("STARVED");
            //script
       // }
       // if (interval > 1)
      //  {
      //      if (stats.hunger > 0)
     //           stats.hunger -= hungerRate;
     //       else
     //       {
     //           stats.hp -= 1;
    //        }

     //       interval = 0;
     //   }
     //   interval += UnityEngine.Time.deltaTime;
        
   // }
}
