//adapted and modified from video "How to make a HEALTH BAR in Unity!" by Brackeys
//https://www.youtube.com/watch?v=BLfNP4Sc_iA
//Author: Sandeep, Travis, Andrew
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
    public KeyCode saveKey = KeyCode.V;
    public KeyCode loadGameKey = KeyCode.B;
    public bool portalWarp;
    
    //Created by Andrew Rodriguez: This function simply calls the static SavePlayer function in our save system class
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    //Created by Andrew Rodriguez & Travis Parks: This function calls the load player stats function in our save system then loads the player stats
    //into the current players data. So their hunger, hp, and position are updated to whatever was last saved.
    public void LoadPlayer()
    {
        if(GetComponent<Movement>().grab.isHolding){
            GetComponent<Movement>().grab.interact.detach();
        }
        PlayerData data = SaveSystem.LoadPlayerStats();
        hunger = data.hunger;
        hp = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        GetComponent<WorldShift>().hollOrReal = data.hollOrReal;

    }

    public void Die(){
        LoadPlayer();
    }
    public void takeDamage(float damage){
        if (hp - damage < 0){
            Debug.Log("Went from "+hp+" to 0");
            hp = 0;
            Die();
            
        }
        else {
            Debug.Log("Went from "+hp+" to "+ Mathf.Round(hp-damage));
            hp = Mathf.Round(hp-damage);
        }
    }

    void Start()
    {
        //Test line to see if we can set a default start point
        SaveSystem.SavePlayer(this);
        //when game is started, it sets the slider max value to hunger value
        hungerBar.SetMaxHunger(hunger);
        //when game is started, it sets the slider max value to hp value
        healthBar.SetMaxHealth(hp);
    }

    void Update()
    {
        Debug.Log(portalWarp);
        //updates the slider value to match the current hunger value
        hungerBar.SetHunger(hunger);
        //updates the slider value to match the current hp value
        healthBar.SetHealth(hp);

        if (Input.GetKeyDown(saveKey))
        {
            SavePlayer();
        }
        else if (Input.GetKey(loadGameKey))
        {
            LoadPlayer();
        }
    }

}
