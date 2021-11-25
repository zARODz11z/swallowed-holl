using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldShift : MonoBehaviour
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
    bool desiresShift;
    bool subDummy = false;
    bool possibleShift;
    [Tooltip("True = holl, false = realWorld")]
    public bool hollOrReal;
    [SerializeField]
    bool shiftBlocked = false;
    public void setShiftBlocked(bool plug){
        shiftBlocked = plug;
    }
    public void setShiftBlockedTrue(){
        shiftBlocked = true;
    }
    public void setShiftBlockedFalse(){
        shiftBlocked = false;
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
        if(hollOrReal){
                dummy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - warpOffset);
        }
        else{
                dummy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + warpOffset);
        }
        if (Input.GetKeyDown(KeyCode.R)){
            if(GetComponent<PlayerStats>().hunger > shiftCost){
                if (hollOrReal){ 
                    desiresShift = true;
                    if(!shiftBlocked){   
                        if(dummy.gameObject.GetComponent<canShift>().getShiftable()){
                            
                            if(dummy.gameObject.GetComponent<canShift>().getCollider() != null){
                                //Debug.Log("Shifting to Breakable Object");
                                dummy.gameObject.GetComponent<canShift>().getCollider().GetComponent<Shatter>().oneShot(0);
                                transform.position = dummy.transform.position;
                                GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
                                hollOrReal = false;
                                
                            }
                            else{
                                //Debug.Log("Shifting to Main dummy");
                                transform.position = dummy.transform.position;
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
                else if (!hollOrReal){ 
                    desiresShift = true; 
                    if(!shiftBlocked){   
                    if(dummy.gameObject.GetComponent<canShift>().getShiftable()){
                        
                        if(dummy.gameObject.GetComponent<canShift>().getCollider() != null){
                            //Debug.Log("Shifting to Breakable Object");
                            dummy.gameObject.GetComponent<canShift>().getCollider().GetComponent<Shatter>().oneShot(0);
                            transform.position = dummy.transform.position;
                            GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
                            hollOrReal = true;
                        }
                        else{
                            //Debug.Log("Shifting to Main dummy");
                            transform.position = dummy.transform.position;
                            GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
                            hollOrReal = true;
                        }

                    }    
                    else {
                        foreach(GameObject d in dummies){
                            if(d.gameObject.GetComponent<canShift>().getShiftable()){
                                //Debug.Log("Shifting to subdummy "+ d);
                                transform.position = d.transform.position;
                                GetComponent<PlayerStats>().hunger = GetComponent<PlayerStats>().hunger - shiftCost;
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
