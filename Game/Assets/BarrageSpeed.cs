using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageSpeed : MonoBehaviour
{
    bool gate;
    float speed = .7f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnDisable() {
        Debug.Log("reset to defaults");
        animator.SetFloat("Speed", 1f );
        speed = .7f;

    }
    void Update(){
        if ( speed < 1f){
            Debug.Log("increasing");
            speed += .0005f;
        }
        if (speed >= 1f){
            Debug.Log("max speed");
            speed = 1f;
        }
        animator.SetFloat("Speed", speed );
            
    }
}
