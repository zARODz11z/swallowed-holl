using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnim : MonoBehaviour
{
    [SerializeField]
    GameObject sphere;
    FPSMovingSphere player;
    bool barragePrep = false;
    bool flipflop = true;
    bool flipflop2 = true;
    bool blocker = true;
    float charge;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = sphere.GetComponent<FPSMovingSphere>();
    }

    void startBarrage(){
        animator.SetBool("barragePrep", false);
        player.isBarraging = true;
        flipflop2 = true;
    }

    void openGate(){
        Debug.Log("OPENED GATE");
        blocker = true;
    }
    void closeGate(){
        blocker = true;
    }

    void waveStartL(){
        blocker = false;
        flipflop = !flipflop;
        animator.SetBool("isPunchingLeft", true);
        Invoke("waveStop", .3f);
    }
    void waveStartR(){
        blocker = false;
        flipflop = !flipflop;
        animator.SetBool("isPunchingRight", true);
        Invoke("waveStop", .3f);

    }
    void waveStop(){
        
        animator.SetBool("isPunchingLeft", false);
        animator.SetBool("isPunchingRight", false);
        blocker = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("mouse 1")){
            if(flipflop2){
                animator.SetBool("barragePrep", true);
                barragePrep = true;
                blocker = false;
                flipflop2 = false;
            }
            else if (!flipflop2){
                player.isBarraging = false;
                animator.SetBool("barragePrep", false);
                barragePrep = false;
                blocker = true;
                flipflop2 = true;
            }
        }
        if ( Input.GetKeyDown("mouse 0") ){
            if(blocker){
                if(flipflop){
                    Debug.Log("Left Punch");
                    Invoke("waveStartL", .1f);
                }
                else if (!flipflop){
                    Debug.Log("Right Punch");
                    Invoke("waveStartR", .1f);
                }
            }
        }
        if ( Input.GetKey("mouse 0") ){
            if(barragePrep){
                animator.SetBool("charging", true);
                
            }
            else if (!barragePrep){
                animator.SetBool("charging", false);
                player.isBarraging = false;
            }
        }
        else if (!Input.GetKey("mouse 0") ){
            animator.SetBool("charging", false);
            player.isBarraging = false;
        }
    }
}
