using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Brian Meginness and Travis Parks
public class Eat : MonoBehaviour
{
    [SerializeField]
    float food = default;

    public void eatFood()
    {
        GetComponentInParent<playerHunger>().increaseHunger(food);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){

            other.gameObject.transform.root.gameObject.GetComponent<Movement>().hungerDive();
            other.gameObject.transform.root.gameObject.GetComponent<playerHunger>().increaseHunger(food);    

            Destroy(gameObject);

        }
        
    }

}
