using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if (((other.gameObject.GetComponent<Rigidbody>() != null) && other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 60f) || this.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 60f){
            oneShot(0);
        }
    }
    public GameObject shatterPrefab;
    [Tooltip("What shattered mesh spawns")]
    public GameObject shatterSpawnPos;
    [Tooltip("Where the shattered mesh spawns")]
    Renderer color;
    float darken = 1f;
    [SerializeField]
    [Tooltip("How many hits a prop will take before breaking")]
    float hitPoints = 2;
    int Damagestate = 0;
    void Start() {
        color = GetComponent<Renderer>();
    }
    public void oneShot(float time){
        //Debug.Log("get one shotted");
        Invoke("spawnShatter", time);
    }
    public void takeDamage(){
        if (Damagestate < hitPoints){
            foreach(Material m in color.materials ){
                m.SetColor("_EmissionColor", Color.grey * darken);
                Damagestate++;
                darken -= .2f;
            }
        }
        else if ( Damagestate >= hitPoints){
            Invoke("spawnShatter", 0);
        }
    }
    void spawnShatter(){
        Instantiate(shatterPrefab, shatterSpawnPos.transform.position, shatterSpawnPos.transform.rotation);
        Destroy(this.gameObject);
    }
   // public void Break(Collision other){
       // if(other.gameObject.tag == "Player" ){
        //    takeDamage();
            
       // }

   // }
    //void OnCollisionEnter(Collision other) {
      //  Break(other);
    //}
}
