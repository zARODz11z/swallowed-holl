using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieOnEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public void DIE(){
        Destroy(this.gameObject);
    }
}
