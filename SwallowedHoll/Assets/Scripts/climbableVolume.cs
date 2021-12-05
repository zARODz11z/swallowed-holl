using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Travis Parks
public class climbableVolume : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            other.transform.root.GetComponent<Movement>().setCanClimb(true);
        }
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            other.transform.root.GetComponent<Movement>().setCanClimb(false);
        }
    }


}
