using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    public int value;
    private HungerDecay hd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hd = collision.gameObject.GetComponent<HungerDecay>();
            hd.changeHunger(value);
            Destroy(gameObject);
        }
    }
}
