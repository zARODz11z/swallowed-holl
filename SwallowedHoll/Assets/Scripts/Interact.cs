using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Travis Parks and Brian Meginness
//this script handles interacting with various objects, such as a button, a lever, a pickupable object, etc. essentially just pressing e on something
public class Interact : MonoBehaviour
{
    //Components
    Movement movement;
    Grab grab;
    HandAnim hand;
    GameObject[] balls;
    [HideInInspector]
    public Transform prop;
    [HideInInspector]
    public Rigidbody propRB;
    [SerializeField]
    [Tooltip("where a grabbed prop attaches")]
    Transform dummy;
    [HideInInspector]
    public disableDynamicBone bone;
    [SerializeField]
    [Tooltip("where the throwing force originates from")]
    public GameObject origin;

    //Variables
    [SerializeField]
    float distance;
    int ballLength;
    [SerializeField]
    [Tooltip("what objects can be picked up by the player")]
    LayerMask mask = default;
    Transform propParent;

    // Start is called before the first frame update
    void Start()
    {  
        //Assign components
        movement = transform.root.GetComponent<Movement>();
        grab = GetComponent<Grab>();
        hand = GetComponent<HandAnim>();
        balls = GameObject.FindGameObjectsWithTag("bball");
        bone = GetComponent<disableDynamicBone>();
        
    }

    //If not already holding an object, get object components, pass to Grab to do the work
    public void pickUp(GameObject obj){
        if (!grab.isHolding) { 
            prop = obj.GetComponent<Transform>();
            propParent = prop.transform.root;
            propRB = obj.GetComponent<Rigidbody>();
            grab.pickUp(dummy, prop, propRB, obj);
        }
    }

    public void foodDetach(){
        propParent.transform.localScale = new Vector3(.005f, .005f, .005f);
        //opposite of the pick up section, just undoing all of that back to its default state
        hand.setisHolding(false);
        grab.foodHoldingPoint.GetChild(0).SetParent(null);
        grab.isHolding = false;
        propRB.isKinematic=(false);
        prop.transform.gameObject.layer = 13;
        foreach ( Transform child in prop.transform){
            child.transform.gameObject.layer = 13;
            foreach ( Transform child2 in child.transform){
                child2.transform.gameObject.layer = 13;
            }
        }
    }
    // this just makes you drop whatever you are holding
    public void detach(){
        grab.sizes = Grab.objectSizes.none;
        //opposite of the pick up section, just undoing all of that back to its default state
        grab.isgrabCharging = false;
        bone.toggle(false);
        hand.setisHolding(false);
        dummy.GetChild(5).SetParent(null);
       // body = prop.gameObject.GetComponent<CustomGravityRigidbody>();
       // body.enabled = true;
        propRB.isKinematic=(false);
        grab.isHolding = false;
        //this may not be super smart, but i am assuming everything you pick up is labeled as a rigid body. If that changes, this should be updated
        prop.transform.gameObject.layer = 13;
        foreach ( Transform child in prop.transform){
            child.transform.gameObject.layer = 13;
            foreach ( Transform child2 in child.transform){
                child2.transform.gameObject.layer = 13;
            }
        }
    }

    //removes the "thru hoop" status of any basketballs you pick up
    void BBallClear(RaycastHit hit){
        foreach(GameObject b in balls){
            if (b.gameObject == hit.transform.gameObject){
                b.gameObject.GetComponent<BBall>().setThruHoop(false);
                break;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("bball").Length != ballLength){
            balls = GameObject.FindGameObjectsWithTag("bball");
            ballLength = balls.Length;
        }

        //IF not paused
        if (!FindObjectOfType<PauseMenu>().isPaused) {
            //IF e pressed
            if (Input.GetKeyDown("e"))
            {
                // if you are not holding anything, and you are not preparing to barrage, and you are not barraging
                if (!grab.isHolding && !hand.barragePrep && !movement.isBarraging)
                {
                    RaycastHit hit;
                    //IF a raycast hits something
                    if (Physics.SphereCast(origin.transform.position, .5f, (dummy.position - origin.transform.position), out hit, distance, mask))
                    {
                        //Get the properties of the something you hit
                        propRB = hit.rigidbody;
                        prop = hit.transform;
                        //IF the thing you hit has a rigidbody that is light enough for the player to hold
                        if (hit.transform.gameObject.GetComponent<Rigidbody>() != null && hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic == false && hit.transform.gameObject.GetComponent<Rigidbody>().mass <= grab.strength && !grab.justThrew)
                        {
                            //Pick it up
                            pickUp(prop.gameObject);
                            BBallClear(hit);
                        }
                        //IF the the thing you hit is a button
                        if (hit.transform.gameObject.GetComponent<buttonPush>() != null)
                        {
                            //Get the button object
                            buttonPush button = hit.transform.gameObject.GetComponent<buttonPush>();
                            if ((button.oneTime && button.anim.GetBool("onePush") == false && button.door.subGate == false) || (button.door != null && button.door.subGate == false) || !button.blocker)
                            {
                                button.press();
                            }
                            if ((button.oneTime && button.anim.GetBool("onePush") == false && button.belt.subGate == false) || (button.belt != null && button.belt.subGate == false) || !button.blocker)
                            {
                                button.press();
                            }
                        }
                    }
                }
                // if you are already holding something, drop it. 
                else if (grab.isHolding && !grab.isFood)
                {
                    detach();
                    //clear the temps for next loop
                    prop = null;
                    propRB = null;
                    grab.throwingforce = grab.throwingTemp;
                }
                else if (grab.isHolding && grab.isFood)
                {
                    foodDetach();
                    prop = null;
                    propRB = null;
                }
            }
        }
    }
}