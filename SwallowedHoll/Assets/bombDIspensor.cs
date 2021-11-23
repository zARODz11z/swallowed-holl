using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombDIspensor : MonoBehaviour
{
    [SerializeField]
    Transform bombSpawn;
    [SerializeField]
    GameObject bomb;
    public void spawnBomb(){
        foreach(GameObject G in GameObject.FindGameObjectsWithTag("Explosive")){
            if(G.GetComponent<Bomb>()!=null){
                G.GetComponent<Shatter>().oneShot(0);
            }
        }
        Instantiate(bomb, bombSpawn.position, Quaternion.identity);
    }

}
