using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script handles interacting with various objects, such as a button, a lever, a pickupable object, etc. essentially just pressing e on something
public class Interact : MonoBehaviour
{
    Movement movement;
    Grab grab;
    HandAnim hand;
    int ballLength;
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
    [SerializeField]
    float distance;
    [SerializeField]
    [Tooltip("what objects can be picked up by the player")]

    LayerMask mask = default;
    // Start is called before the first frame update
    void Start()
    {  
        movement = transform.root.GetComponent<Movement>();
        grab = GetComponent<Grab>();
        hand = GetComponent<HandAnim>();
        balls = GameObject.FindGameObjectsWithTag("bball");
        bone = GetComponent<disableDynamicBone>();
        
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


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("bball").Length != ballLength){
            balls = GameObject.FindGameObjectsWithTag("bball");
            ballLength = balls.Length;
        }
        if (Input.GetKeyDown("e"))
        {
            // if you are not holding anything, and you are not preparing to barrage, and you are not barraging
            if(!grab.isHolding && !hand.barragePrep && !movement.isBarraging){
            RaycastHit hit;
            if (Physics.SphereCast(origin.transform.position, 1, (dummy.position - origin.transform.position), out hit, distance, mask))
            {
                if(hit.transform.gameObject.GetComponent<Rigidbody>() != null && hit.transform.gameObject.GetComponent<Rigidbody>().mass <= grab.strength && !grab.justThrew){
                    prop = hit.transform;
                    propRB = hit.rigidbody;
                    grab.pickUp(origin, dummy, prop, propRB, balls, hit);
                }
            }
        }
            // if you are already holding something, drop it. 
            else if (grab.isHolding){
                detach();
                //clear the temps for next loop
                prop = null;
                propRB = null;
                grab.throwingforce = grab.throwingTemp;
            }
        
        }
    }
}