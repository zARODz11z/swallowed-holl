using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            other.transform.root.GetComponent<PlayerStats>().LoadPlayer();
        }
    }
}
