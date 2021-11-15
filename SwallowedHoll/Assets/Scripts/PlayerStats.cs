//adapted and modified from video "How to make a HEALTH BAR in Unity!" by Brackeys
//https://www.youtube.com/watch?v=BLfNP4Sc_iA
//Author: Sandeep and Travis
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will just keep track of the player's various stats and allow other scripts to access and edit them
public class PlayerStats : MonoBehaviour
{
    public float hunger = 100;
    public float hp = 100;

    //calls the HungerBar class and assigns it to hungerBar to use its methods
    public HungerBar hungerBar;
    //calls the HealthBar class and assigns it to healthBar to use its methods
    public HealthBar healthBar;

    void Start()
    {
      //when game is started, it sets the slider max value to hunger value
      hungerBar.SetMaxHunger(hunger);
      //when game is started, it sets the slider max value to hp value
      healthBar.SetMaxHealth(hp);
    }

    void Update()
    {
      //updates the slider value to match the current hunger value
      hungerBar.SetHunger(hunger);
      //updates the slider value to match the current hp value
      healthBar.SetHealth(hp);
    }

}
