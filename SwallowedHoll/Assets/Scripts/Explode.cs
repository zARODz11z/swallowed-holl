using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    Rigidbody body; 
    [SerializeField]
    float radius;
    [SerializeField]
    float power; 
    [SerializeField]
    float upModifier;
    [SerializeField]
    bool isBomb;
    GameObject player;  
    [SerializeField]
    float playerDamageMax, playerDamageMin;
    bool gate;
    float damage;
    //[SerializeField]
    //float otherExplosiveTime = 1f;
    //Shatter otherExplosive;
    // Start is called before the first frame update

    void ragdollBlast(){
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(hit.tag == "ragdoll"){
                rb.AddExplosionForce(power/2, explosionPos, radius, upModifier);
            }
        }
    }
    void Start()
    {
        foreach (GameObject G in GameObject.FindGameObjectsWithTag("Player")){
            if(G.GetComponent<Movement>() != null){
                player = G;
            }
        }
        if((player.transform.position - this.transform.position).magnitude/25 <=1){
            damage = (player.transform.position - this.transform.position).magnitude/25;
        }
        else if((player.transform.position - this.transform.position).magnitude/25  <= 0 ){
            damage = 0;
        }
        else{
            damage = 1;
        }

        body = GetComponent<Rigidbody>();
        Vector3 explosionPos = transform.position;
        	Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        	foreach (Collider hit in colliders)
        	{
                if(hit.gameObject.tag == "NPC" && hit.gameObject.transform.parent.gameObject.GetComponent<ragdollSpawn>() != null && isBomb){
                    hit.gameObject.transform.parent.gameObject.GetComponent<ragdollSpawn>().spawn();
                    ragdollBlast();
                }
            	Rigidbody rb = hit.GetComponent<Rigidbody>();
                if(!isBomb){
                    if (hit.transform.IsChildOf(this.transform)){
                        if (rb != null)
                            rb.AddExplosionForce(power, explosionPos, radius, upModifier);
                        }
                }
                else{
                    if(hit.gameObject.GetComponent<Shatter>() != null && hit.gameObject.GetComponent<Shatter>().bombBreak && isBomb){
                        hit.gameObject.GetComponent<Shatter>().oneShot(0);
                    }
                    if (rb != null){
                        //if (rb.gameObject.tag == "Explosive"){
                        //    otherExplosive = rb.gameObject.GetComponent<Shatter>();
                        //    otherExplosive.oneShot(otherExplosiveTime);
                       // }
                    rb.AddExplosionForce(power, explosionPos, radius, upModifier);
                    if(!gate){
                        player.GetComponent<PlayerStats>().takeDamage(Mathf.Lerp(playerDamageMax, playerDamageMin, damage));
                        //Debug.Log("Lerping from "+ damage);
                        //Debug.Log("Reduce"+ Mathf.Lerp(playerDamageMax, playerDamageMin, damage));
                        gate = true;
                    }
                }
            }
        }
        gate = false;
    }
    // Update is called once per frame
}
