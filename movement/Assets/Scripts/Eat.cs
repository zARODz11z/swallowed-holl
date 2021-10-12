using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    [SerializeField]
    float food;
    [SerializeField]
    GameObject hunger;
    playerHunger player;
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            player.increaseHunger(food);
            Destroy(gameObject);
        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = hunger.GetComponent<playerHunger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
