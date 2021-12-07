using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Brian Meginness and Travis Parks
public class Eat : MonoBehaviour
{
    [SerializeField]
    float food = default;

    //Eats the object, destroying it and adding the appropriate value to player's hunger
    public void eatFood()
    {
        GetComponentInParent<playerHunger>().increaseHunger(food);
        Destroy(gameObject);
    }

    //If the food item is a floating object instead of a physical one, consume on contact
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            //Call the player's hungerDive() method to see if they should bounce on contant
            other.gameObject.transform.root.gameObject.GetComponent<Movement>().hungerDive();
            //Increase the player's hunger value
            other.gameObject.transform.root.gameObject.GetComponent<playerHunger>().increaseHunger(food);    
            //Destroy the food object
            Destroy(gameObject);

        }
        
    }

}