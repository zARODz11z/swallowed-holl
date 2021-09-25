using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBuster : MonoBehaviour
{
    [SerializeField]
    bool oneShot = false;
    Shatter otherExplosive;
    void OnCollisionEnter(Collision other) {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Breakable" || other.gameObject.tag == "Explosive"){
            Debug.Log("TEST");
            if(!oneShot){
                otherExplosive = other.gameObject.GetComponent<Shatter>();
                otherExplosive.takeDamage();
            }
            else{
                otherExplosive = other.gameObject.GetComponent<Shatter>();
                otherExplosive.oneShot(0);
            }
        }
    }
}

