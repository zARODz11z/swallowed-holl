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
    bool isHolding = false;
    [SerializeField]
    float throwingforce = 5;

    void detach(){
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
        RaycastHit hit;
        if (Input.GetKeyDown("e"))
        {
            if(!isHolding){
                if (Physics.Raycast(origin.transform.position, (dummy.position - origin.transform.position), out hit, distance, mask))
                {
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
            }
        }
    }
}
