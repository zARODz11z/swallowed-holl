using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script moves the object upwards at a set rate until it reaches a certain threshold, then it pauses once it reaches that point
public class DoorOpenandClose : MonoBehaviour
{
    [SerializeField]
    float rate;
    Animator anim;
    bool gate;
    bool direction;
    float counter = 0f;
    [SerializeField]
    float heightStopPoint;  
    [HideInInspector]
    public bool subGate;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update() {
        if(gate){
            if(direction){
                if(counter < heightStopPoint){
                    this.transform.position = new Vector3(this.transform.position.x , this.transform.position.y + rate * Time.deltaTime, this.transform.position.z);
                    counter += rate * Time.deltaTime;
                }
                else if (counter >= heightStopPoint){
                    gate = false;
                    counter = 0;
                    subGate = false;
                }
            }
            else if(direction == false){
                if(counter > -heightStopPoint){
                    this.transform.position = new Vector3(this.transform.position.x , this.transform.position.y - rate * Time.deltaTime, this.transform.position.z);
                    counter -= rate * Time.deltaTime;
                }
                else if (counter <= -heightStopPoint){
                    gate = false;
                    counter = 0;
                    subGate = false;
                }
            }
        }
    }
    public void openSlidingDoor(){
        if(!subGate){
            gate = true;
            direction = true;
            counter = 0;
            subGate = true;
        }
        
    }
    public void closeSlidingDoor(){
        if(!subGate){
            gate = true;
            direction = false;
            counter = 0;
            subGate = true;
        }
    }
}
