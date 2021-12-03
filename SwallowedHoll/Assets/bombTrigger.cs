using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Explosive"){
            other.gameObject.GetComponent<Shatter>().oneShot(0);
        }
    }
}
