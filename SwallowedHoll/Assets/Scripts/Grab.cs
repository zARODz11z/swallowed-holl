using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Travis Parks and Brian Meginness
// This script deals with holding objects after you have interacted with an object that has a rigidbody, is tagged as pickupable, and isn't over your max carrying weight.
// It pins that object to an empty tied to the player, creates a collider to represent that object while it is in your hands, and switches the player to an animation set to 
// reflect that they are holding something. It also disables dynamic bones while you are holding something. This script also handles the logic for throwing objects, including charging up and releasing
public class Grab : MonoBehaviour
{
    //Components
    MovementSpeedController speedController;
    Material mat;
    CustomGravityRigidbody body;
    HandAnim hand;
    Movement movement;

    [HideInInspector]
    public bool isHolding = false;
    [SerializeField]
    public float throwingforce = 5;
    
    //Throwing variables
    [Tooltip("the point that a fully charged throw will head toward")]
    [SerializeField]
    Transform LowthrowingPoint;
    [SerializeField]
    [Tooltip("the point that a light toss will head toward")]
    Transform HighthrowingPoint;
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

    //Object sizes
    public enum objectSizes{tiny, small, medium, large, none};
    public objectSizes sizes;

    Interact interact;

    //Is held object food
    private bool isFood;
    public bool justThrew;


    void Start() {
        //Set components
        interact = GetComponent<Interact>();
        throwingTemp = throwingforce;
        movement = transform.root.GetComponent<Movement>();
        hand = GetComponent<HandAnim>();
    }

    void setisThrowingFalse(){
        hand.setisThrowing(false);
    }
    void resetJustThrew(){
        justThrew = false;
    }

    public void pickUp(GameObject origin, Transform dummy, RaycastHit hit){
        Transform prop = hit.transform;
        Rigidbody propRB = hit.rigidbody;
        GameObject propGame = hit.transform.gameObject;

        //Get size of held object
        if(propGame.GetComponent<objectSize>().sizes == objectSize.objectSizes.large){
            sizes = objectSizes.large;
        }
        if(propGame.GetComponent<objectSize>().sizes == objectSize.objectSizes.medium){
            sizes = objectSizes.medium;
        }
        if(propGame.GetComponent<objectSize>().sizes == objectSize.objectSizes.small){
            sizes = objectSizes.small;                
        }     
        if(propGame.GetComponent<objectSize>().sizes == objectSize.objectSizes.tiny){
            sizes = objectSizes.tiny;
        }              
        //Is the held object something you can eat?
        if(propGame.GetComponent<Eat>()){
            isFood = true;
        }
        else{
            isFood = false;
        }
        //disable dynamic bones
        interact.bone.toggle(true);
        //trigger animation
        hand.setisHolding(true);
        // move the hit object to the grab point
        prop.position = dummy.transform.position;
        // set the hit object to be a child of the grab point
        prop.SetParent(dummy);
        // get a reference to the custom gravity rigidbody to disable gravity and sleeping
        propRB.isKinematic=(true);
        isHolding = true;
        // set the held object to the "nocollidewithplayer" layer to prevent clipping with the player
        propGame.layer = 16;
        // do the same for all children and childrens children 
        foreach ( Transform child in prop){
            child.transform.gameObject.layer = 16;
            foreach ( Transform child2 in child.transform){
                child2.transform.gameObject.layer = 16;
            }
        }

    }
    void Update()
    {   //IF Left Mouse released and is holding an object
        if (Input.GetKeyUp("mouse 0") && isHolding && !hand.barragePrep && !movement.isBarraging && !justThrew){
            //Remove from grip
            interact.detach();
            //Add appropriate force to object
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
            justThrew = true;
            Invoke("resetJustThrew", .5f);
        }
        //IF Left Mouse pressed and is holding an object
        if (Input.GetKey("mouse 0") && isHolding && !hand.barragePrep && !movement.isBarraging && !justThrew){
            // Start incrementing throwing force
            if (throwingforce <= maxThrowingForce){
                isgrabCharging = true;
                throwingforce = throwingforce + chargeRate;
            }
            if (throwingforce > maxThrowingForce){
                isgrabCharging = false;
                highorLow = false;
            }
        }

        //IF Right Mouse pressed and is holding food
        if(Input.GetKey("mouse 1") && isHolding && !hand.barragePrep && !movement.isBarraging && isFood){
            //Eat food and remove from hands
            interact.prop.gameObject.GetComponent<Eat>().eatFood();
            interact.detach();
        }

    }
}
