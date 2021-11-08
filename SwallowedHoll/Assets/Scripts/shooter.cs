using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{

    [SerializeField]
    GameObject ammo;
    [SerializeField]
    GameObject ammo2;
    [SerializeField]
    GameObject ammo3;
    [SerializeField]
    GameObject ammo4;
    [SerializeField]
    Transform spawnPos;

    // Start is called before the first frame update
    // Update is called once per frame
    public void spawnKetchup(){
            GameObject clone;
            clone = Instantiate(ammo, spawnPos.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody>().AddForce(spawnPos.transform.up * 50f, ForceMode.Impulse); 
    }
    public void spawnTV(){
            GameObject clone;
            clone = Instantiate(ammo2, spawnPos.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody>().AddForce(spawnPos.transform.up * 50f, ForceMode.Impulse); 
    }
    public void spawnSoda(){
            GameObject clone;
            clone = Instantiate(ammo3, spawnPos.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody>().AddForce(spawnPos.transform.up * 50f, ForceMode.Impulse); 
    }
    public void spawnBBall(){
            GameObject clone;
            clone = Instantiate(ammo4, spawnPos.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody>().AddForce(spawnPos.transform.up * 50f, ForceMode.Impulse); 
    }

}
