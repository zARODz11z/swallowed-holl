using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Brian Meginness and Travis Parks
public class Eat : MonoBehaviour
{
    [SerializeField]
    float food = default;

    //Eats the object, destroying it and adding the appropriate value to player's hunger
    public void respawnFood(){
        this.GetComponent<Renderer>().enabled = true;
        this.GetComponent<BoxCollider>().enabled = true;
    }
    public void hideFood(Collider other){
        other.transform.root.GetComponent<playerHunger>().increaseHunger(food);
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        Invoke("respawnFood", 5f);
    }
    public void eatFood()
    {
        GetComponentInParent<playerHunger>().increaseHunger(food);
        Destroy(gameObject);
    }

    //If the food item is a floating object instead of a physical one, consume on contact
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" &&( ((other.transform.root.GetComponent<PlayerStats>().hunger + food) < 100) || other.gameObject.transform.root.gameObject.GetComponent<Movement>().Diving)){
            //Call the player's hungerDive() method to see if they should bounce on contant
            other.gameObject.transform.root.gameObject.GetComponent<Movement>().hungerDive();
            //Increase the player's hunger value
            hideFood(other);

        }
        
    }

}
