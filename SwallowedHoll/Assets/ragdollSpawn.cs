using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollSpawn : MonoBehaviour
{
    [SerializeField]
    bool isProtected;
    [SerializeField]
    GameObject ragdoll;
    [SerializeField]
    Transform spawnPos;
    // Start is called before the first frame update
    public void spawn(){
        if(!isProtected){
            Instantiate(ragdoll, spawnPos.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
