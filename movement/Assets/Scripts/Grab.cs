using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    //renda.material.Lerp(renda.material, shadow, .5f);
    [SerializeField]
    float distance;
    [SerializeField]
    LayerMask mask = default;
    

    [SerializeField]
    Transform dummy;
    [SerializeField]
    GameObject origin;
    Transform prop;
    Rigidbody propRB;
    CustomGravityRigidbody body;
    Renderer renda;
    [HideInInspector]
    public bool isHolding = false;
    [SerializeField]
    float throwingforce = 5;
    HandAnim hand;
    FPSMovingSphere player;
    disableDynamicBone bone;
    

    void Start() {
        player = transform.parent.transform.parent.GetComponent<FPSMovingSphere>();
        hand = GetComponent<HandAnim>();
        bone = GetComponent<disableDynamicBone>();
    }
    void setisThrowingFalse(){
        hand.setisThrowing(false);
    }


    void detach(){
        bone.toggle(false);
        hand.setisHolding(false);
        dummy.DetachChildren();
        Debug.Log("dropped");
        body = prop.gameObject.GetComponent<CustomGravityRigidbody>();
        body.enabled = true;
        propRB.isKinematic=(false);
        isHolding = false;
        prop.transform.gameObject.layer = 13;

    }

    void Update()
    {
        Debug.Log(hand.barragePrep);
        RaycastHit hit;
        if (Input.GetKeyDown("e"))
        {
            if(!isHolding && !hand.barragePrep && !player.isBarraging){
                if (Physics.Raycast(origin.transform.position, (dummy.position - origin.transform.position), out hit, distance, mask))
                {
                    bone.toggle(true);
                    hand.setisHolding(true);
                    Debug.DrawLine(origin.transform.position, hit.point, Color.white, 3f);
                    Debug.Log("pickup");
                    prop = hit.transform;
                    propRB = hit.rigidbody;
                    hit.transform.position = dummy.transform.position;
                    hit.transform.SetParent(dummy);
                    body = hit.transform.gameObject.GetComponent<CustomGravityRigidbody>();
                    renda = prop.gameObject.GetComponent<Renderer>();
                    body.enabled = false;
                    propRB.isKinematic=(true);
                    isHolding = true;
                    prop.transform.gameObject.layer = 16;
                }
            }
            else if (isHolding){
                detach();
                prop = null;
                propRB = null;
            }

        }
        if (Input.GetKeyDown("mouse 2")){
            if(isHolding){
                detach();
                propRB.velocity = (dummy.position - origin.transform.position) * throwingforce;
                hand.setisThrowing(true);
                Invoke("setisThrowingFalse", .5f);

            }
        }
    }
}
