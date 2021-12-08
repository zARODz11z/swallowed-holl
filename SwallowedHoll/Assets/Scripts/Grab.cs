using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Travis Parks and Brian Meginness
// This script deals with holding objects after you have interacted with an object that has a rigidbody, is tagged as pickupable, and isn't over your max carrying weight.
// It pins that object to an empty tied to the player, creates a collider to represent that object while it is in your hands, and switches the player to an animation set to 
// reflect that they are holding something. It also disables dynamic bones while you are holding something. This script also handles the logic for throwing objects, including charging up and releasing
public class Grab : MonoBehaviour
{
    [SerializeField] private AudioSource[] eatingAudioSource;
    [SerializeField]
    AudioSource[] pickUpAudioSource;
    [SerializeField]
    AudioSource[] throwAudioSource;
    //Components
    HandAnim hand;
    Movement movement;
    Controls controls;

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
    [HideInInspector]
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
    [HideInInspector]
    public bool isgrabCharging = false;

    //Object sizes
    public enum objectSizes{tiny, small, medium, large, none};
    [HideInInspector]
    public objectSizes sizes;

    public Interact interact;

    //Is held object food
    [HideInInspector]
    public bool isFood;
    [HideInInspector]
    public bool justThrew;
    [SerializeField]
    public Transform foodHoldingPoint;

    void Start() {
        //Set components
        controls = GameObject.Find("Player").GetComponentInChildren<Controls>();
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

    public void pickUp(Transform dummy, Transform prop, Rigidbody propRB, GameObject propGame)
    {    
        int index = Random.Range(0, pickUpAudioSource.Length - 1);
        pickUpAudioSource[index].Play();  

        //Is the held object something you can eat?
        if(propGame.GetComponent<Eat>()){
            isFood = true;
        }
        else{
            isFood = false;
        }
        if(!isFood){
            //Debug.Log("Holding not food");
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
        else{
            Debug.Log("Holding Food");
            propRB.isKinematic=(true);
            prop = prop.transform.root.transform;
            propGame = propGame.transform.root.gameObject;
            prop.localScale = new Vector3 (.25f, .25f, .25f);
            propGame.GetComponent<Floater>().enabled = false;
            hand.setisHolding(true);
            prop.position = foodHoldingPoint.position;
            isHolding = true;
            propGame.layer = 15;
            foreach ( Transform child in prop){
                child.transform.gameObject.layer = 15;
                foreach ( Transform child2 in child.transform){
                    child2.transform.gameObject.layer = 15;
                }
            }
            prop.SetParent(foodHoldingPoint);
        }

    }
    //called in eating animation
    public void eatFood(){
        interact.foodDetach();
        interact.prop.gameObject.GetComponent<Eat>().eatFood();

        int index = Random.Range(0, eatingAudioSource.Length - 1);
        Debug.Log(index);
        eatingAudioSource[index].Play();
        eatingAudioSource[5].Play();
    }
    void Update()
    {
        //IF not paused
        if (!FindObjectOfType<PauseMenu>().isPaused)
        {
            //IF Left Mouse released and is holding an object
            if (Input.GetKeyUp(controls.keys["throw"]) && isHolding && !hand.barragePrep && !movement.isBarraging && !justThrew && !isFood)
            {
                int index = Random.Range(0, throwAudioSource.Length - 1);
                throwAudioSource[index].Play(); 
                
                //Remove from grip
                interact.detach();
                //Add appropriate force to object
                if (highorLow)
                {
                    interact.propRB.AddForce((HighthrowingPoint.position - interact.origin.transform.position) * throwingforce, ForceMode.Impulse);
                }
                else
                {
                    interact.propRB.AddForce((LowthrowingPoint.position - interact.origin.transform.position) * throwingforce, ForceMode.Impulse);
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
            if (Input.GetKey(controls.keys["throw"]) && isHolding && !hand.barragePrep && !movement.isBarraging && !justThrew && !isFood)
            {
                // Start incrementing throwing force
                if (throwingforce <= maxThrowingForce)
                {
                    isgrabCharging = true;
                    throwingforce = throwingforce + chargeRate;
                }
                if (throwingforce > maxThrowingForce)
                {
                    isgrabCharging = false;
                    highorLow = false;
                }
            }
            else if (Input.GetKey(controls.keys["throw"]) && isHolding && !hand.barragePrep && !movement.isBarraging && !justThrew && isFood)
            {
                interact.foodDetach();
                interact.propRB.AddForce((LowthrowingPoint.position - interact.origin.transform.position) * throwingforce, ForceMode.Impulse);
                hand.setisThrowing(true);
                Invoke("setisThrowingFalse", .1f);
                justThrew = true;
                Invoke("resetJustThrew", .5f);
            }

            //IF Right Mouse pressed and is holding food
            if (Input.GetKey(controls.keys["eat"]) && isHolding && !hand.barragePrep && !movement.isBarraging && isFood)
            {
                hand.setEatFood();
            }
        }
    }
}
