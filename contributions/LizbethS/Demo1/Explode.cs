using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    Rigidbody body;
    [SerializeField]  //This allows the player to change the variable on Unity
    float radius;     // Creates float variable radius
    [SerializeField]
    float power;      // Creates float variable power
    [SerializeField]
    float upModifier; // Creates float variable upModifier
    [SerializeField]
    bool isBomb;      // Creates boolean isBomb
    [SerializeField]
    float otherExplosiveTime = 1f; // Creates float variable otherExplosiveTime and sets it to 1
    Shatter otherExplosive;     // Creates instance of Shatter object

    // Start is called before the first frame update
    void Start()
    {

        body = GetComponent<Rigidbody>();

        //creates 3D vector that stores coordinates of position
        Vector3 explosionPos = transform.position;

          // Creates collider array to detect all collisions
          //Array adds each object that overlaps with the sphere of the radius of the explosion
        	Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
          // Each object of collider array is checked
        	foreach (Collider hit in colliders)
        	{
            	Rigidbody rb = hit.GetComponent<Rigidbody>();
                if(!isBomb){  //checks to see if object is not a bomb
                    if (hit.transform.IsChildOf(this.transform)){  //checks to see if it is a child of exploding object
                        if (rb != null)     // checks to see if the object has a rigid body
                        //if it has a rigid body, then it applies the explosion force with makes it move
                            rb.AddExplosionForce(power, explosionPos, radius, upModifier);
                        }
                }
                else{
                    if (rb != null){ // checks to see if the object has a rigid body
                        //checks to see if the object is an explosive
                        if (rb.gameObject.tag == "Explosive"){
                            otherExplosive = rb.gameObject.GetComponent<Shatter>();
                            otherExplosive.oneShot(otherExplosiveTime);
                        }
                    rb.AddExplosionForce(power, explosionPos, radius, upModifier);
                    }
                }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}