using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    MovementSpeedController speedController;
    Material mat;
    //[SerializeField]
    //float distance;
    //[SerializeField]
    //[Tooltip("what objects can be picked up by the player")]

    //LayerMask mask = default;

    //[SerializeField]
    //Transform dummy;
    //[SerializeField]
    //[Tooltip("where the throwing force originates from")]

    //GameObject origin;
    //Transform prop;
    //Rigidbody propRB;
    CustomGravityRigidbody body;
    //Renderer renda;
    [HideInInspector]
    public bool isHolding = false;
    [SerializeField]
    public float throwingforce = 5;
    HandAnim hand;
    Movement movement;
    //disableDynamicBone bone;
    [Tooltip("the point that a fully charged throw will head toward")]

    [SerializeField]
    Transform LowthrowingPoint;
    [SerializeField]
    [Tooltip("the point that a light toss will head toward")]

    Transform HighthrowingPoint;
    //GameObject[] balls;
    //int ballLength;
    [SerializeField]
    bool highorLow = true;
    public float throwingTemp;
    [SerializeField]
    [Tooltip("the heaviest possible object the player can pick up")]

    public float strength;
    
    [SerializeField]
    [Tooltip("the maximum force the player can throw an object at, when fully charged")]
    float maxThrowingForce;
    
    [SerializeField]
    [Tooltip("the rate at which the players throw charges")]
    float chargeRate;
    public bool isgrabCharging = false;

    public enum objectSizes{tiny, small, medium, large, none};
    
    public objectSizes sizes;

    Interact interact;

    void Start() {
        interact = GetComponent<Interact>();
        throwingTemp = throwingforce;
        movement = transform.root.GetComponent<Movement>();
        hand = GetComponent<HandAnim>();
    }

    void setisThrowingFalse(){
        hand.setisThrowing(false);
    }

    public void pickUp(GameObject origin, Transform dummy, Transform prop, Rigidbody propRB, GameObject[] balls, RaycastHit hit){
        if(hit.transform.gameObject.GetComponent<objectSize>().sizes == objectSize.objectSizes.large){
            sizes = objectSizes.large;
        }
        if(hit.transform.gameObject.GetComponent<objectSize>().sizes == objectSize.objectSizes.medium){
            sizes = objectSizes.medium;
        }
        if(hit.transform.gameObject.GetComponent<objectSize>().sizes == objectSize.objectSizes.small){
            sizes = objectSizes.small;                
        }     
        if(hit.transform.gameObject.GetComponent<objectSize>().sizes == objectSize.objectSizes.tiny){
            sizes = objectSizes.tiny;
        }                   
        //disable dynamic bones
        interact.bone.toggle(true);
        //trigger animation
        hand.setisHolding(true);
        // move the hit object to the grab point
        hit.transform.position = dummy.transform.position;
        // set the hit object to be a child of the grab point
        hit.transform.SetParent(dummy);
        // get a reference to the custom gravity rigidbody to disable gravity and sleeping
        propRB.isKinematic=(true);
        isHolding = true;
        // set the held object to the "nocollidewithplayer" layer to prevent clipping with the player
        prop.transform.gameObject.layer = 16;
        // do the same for all children and childrens children 
        foreach ( Transform child in prop.transform){
            child.transform.gameObject.layer = 16;
            foreach ( Transform child2 in child.transform){
                child2.transform.gameObject.layer = 16;
            }
        // find if you grabbed a basketball. If so, disable it's "thruHoop" status
        }
        foreach(GameObject b in balls){
            if (b.gameObject == hit.transform.gameObject){
                b.gameObject.GetComponent<BBall>().setThruHoop(false);
                break;
            }
        }
    }
    void Update()
    {   //THROW
        if (Input.GetKeyUp("mouse 0") && isHolding && !hand.barragePrep && !movement.isBarraging ){
            interact.detach();
            if (highorLow){
                interact.propRB.AddForce((HighthrowingPoint.position - interact.origin.transform.position ) * throwingforce, ForceMode.Impulse);
            }
            else{
                interact.propRB.AddForce((LowthrowingPoint.position - interact.origin.transform.position ) * throwingforce, ForceMode.Impulse);
            }         
            // trigger animation
            hand.setisThrowing(true);
            //prepare to reset animation
            Invoke("setisThrowingFalse", .1f);
            throwingforce = throwingTemp;
            highorLow = true;
        }
        //throw charge
        if (Input.GetKey("mouse 0") && isHolding && !hand.barragePrep && !movement.isBarraging ){
            if (throwingforce <= maxThrowingForce){
                isgrabCharging = true;
                throwingforce = throwingforce + chargeRate;
            }
            if (throwingforce > maxThrowingForce){
                isgrabCharging = false;
                highorLow = false;
            }
        }


    }
}
