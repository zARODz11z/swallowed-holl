using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBoomBlock : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Explosive"){
            other.gameObject.GetComponent<Shatter>().setBoomBlocked(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Explosive"){
            other.gameObject.GetComponent<Shatter>().setBoomBlocked(false);
        }
        
    }
}
