using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    GameObject player;
    private void Start() {
        foreach (GameObject G in GameObject.FindGameObjectsWithTag("Player")){
            if(G.GetComponent<Movement>() != null){
                player = G;
            }
        }
    }
    private void Update() {
        if(player.GetComponent<ZoneWarp>().hollOrReal == true && GetComponent<Rigidbody>().isKinematic == true){
            GetComponent<Shatter>().oneShot(0);
        }
    }
}
