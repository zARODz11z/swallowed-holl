using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script controls the world shift mechanic and allows the player to shift between worlds. really you are just being teleported along the y axis, but there are dummy objects checking if you would end up colliding with something else
//if youare in the clear and none of the dummies are colliding with anything, you will just teleport directly to the root dummy. if the root dummy is colliding with something, the script willl check and find a "sub dummy" that is not 
//colliding with anything, and shift to that instead. if all of the dummies are colliding with something, you simply cant shift. 
public class ImprovedZoneWarp : MonoBehaviour
{

    [SerializeField]
    float shiftCost;
    Collider other = null;
    [SerializeField]
    GameObject dummy;
    [SerializeField]
    GameObject[] dummies;
    [SerializeField]
    float warpOffset;
    bool flipflop;
    bool desiresShift;
    bool subDummy = false;
    bool possibleShift;
    [Tooltip("True = holl, false = realWorld")]
    bool hollOrReal;
    bool shiftBlocked = false;
    public void setShiftBlocked(bool plug){
        shiftBlocked = plug;
    }
    public void setOther(Collider oth){
        other = oth;
    }
    public bool getDesiresShift(){
        return desiresShift;
    }
    public void setDesiresShift(bool plug){
        desiresShift = plug;
    }
    void Update()
    {
        if(flipflop){
                dummy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - warpOffset);
        }
        else{
                dummy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + warpOffset);
        }
        if (Input.GetKeyDown(KeyCode.R)){
            if(GetComponent<PlayerStats>().hunger > shiftCost){
                if (flipflop){ 
                    desiresShift = true;
                    if(!shiftBlocked){   
                        if(dummy.gameObject.GetComponent<canShift>().getShiftable()){
                            
                            if(dummy.gameObject.GetComponent<canShift>().getCollider() != null){
                                //Debug.Log("Shifting to Breakable Object");
                                dummy.gameObject.GetComponent<canShift>().getCollider().GetComponent<Shatter>().oneShot(0);
                                transform.position = dummy.transform.position;
                                flipflop = false;
                                GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
                                hollOrReal = false;
                                
                            }
                            else{
                                //Debug.Log("Shifting to Main dummy");
                                transform.position = dummy.transform.position;
                                flipflop = false;
                                GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
                                hollOrReal = false;
                            }

                        }    
                        else {
                            foreach(GameObject d in dummies){
                                if(d.gameObject.GetComponent<canShift>().getShiftable()){
                                    //Debug.Log("Shifting to subdummy "+ d);
                                    transform.position = d.transform.position;
                                    GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
                                    flipflop = false;
                                    subDummy = true;
                                    hollOrReal = false;
                                    return;
                                }
                                else{
                                    subDummy = false;
                                }
                            }

                        }
                        if(subDummy == false){
                            //Debug.Log("Cant Shift!");
                        }
                    }
                    else{
                        //Debug.Log("Shift Blocked!");
                    }
                }          
                else if (!flipflop){ 
                    desiresShift = true; 
                    if(!shiftBlocked){   
                    if(dummy.gameObject.GetComponent<canShift>().getShiftable()){
                        
                        if(dummy.gameObject.GetComponent<canShift>().getCollider() != null){
                            //Debug.Log("Shifting to Breakable Object");
                            dummy.gameObject.GetComponent<canShift>().getCollider().GetComponent<Shatter>().oneShot(0);
                            transform.position = dummy.transform.position;
                            GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
                            flipflop = true;
                            hollOrReal = true;
                        }
                        else{
                            //Debug.Log("Shifting to Main dummy");
                            transform.position = dummy.transform.position;
                            GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
                            flipflop = true;
                            hollOrReal = true;
                        }

                    }    
                    else {
                        foreach(GameObject d in dummies){
                            if(d.gameObject.GetComponent<canShift>().getShiftable()){
                                //Debug.Log("Shifting to subdummy "+ d);
                                transform.position = d.transform.position;
                                GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
                                flipflop = true;
                                subDummy = true;
                                hollOrReal = true;
                                return;
                            }
                            else{
                                subDummy = false;
                            }
                        }

                    }
                    if(subDummy == false){
                        //Debug.Log("Cant Shift!");
                            }
                        }   
                        else{
                            //Debug.Log("Shift Blocked");
                        }
                    } 
                }  
                else{
                    Debug.Log("Not enough hunger to shift!");
                }    
            }
        else{
            desiresShift = false;
        }
        if (dummies[0].gameObject.GetComponent<canShift>().getShiftable() || dummies[1].gameObject.GetComponent<canShift>().getShiftable() || dummies[2].gameObject.GetComponent<canShift>().getShiftable() || dummies[3].gameObject.GetComponent<canShift>().getShiftable() || dummies[4].gameObject.GetComponent<canShift>().getShiftable() || dummy.gameObject.GetComponent<canShift>().getShiftable()){
            possibleShift = true;
        }
        else{
            possibleShift = false;
        }
    }
}

