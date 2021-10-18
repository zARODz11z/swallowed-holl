using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float boost = 50f;
    [SerializeField]
    [Tooltip("amount of spin added to objects on the conveyor belt")]
    float spinAmount;
    List<GameObject> pushingObjects = new List<GameObject>();
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<Rigidbody>() != null && other.gameObject.GetComponent<Rigidbody>().isKinematic == false && other.gameObject.layer != 16){
            pushingObjects.Add(other.gameObject);
        }
        if(other.gameObject.tag == "Player"){
            pushingObjects.Add(other.gameObject.transform.root.gameObject);
        }
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject.GetComponent<Rigidbody>() != null && other.gameObject.GetComponent<Rigidbody>().isKinematic == false && other.gameObject.layer != 16){
            Debug.Log("REMOVED VIA EXIT");
            pushingObjects.Remove(other.gameObject);
            other.gameObject.GetComponent<Rigidbody>().velocity = other.gameObject.GetComponent<Rigidbody>().velocity + this.transform.right * (speed);
        }
        if(other.gameObject.tag == "Player"){
            Debug.Log("REMOVED VIA EXIT");
            pushingObjects.Remove(other.gameObject.transform.root.gameObject);
            other.gameObject.transform.root.gameObject.GetComponent<Rigidbody>().velocity = other.gameObject.transform.root.gameObject.GetComponent<Rigidbody>().velocity + this.transform.right * (speed);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pushingObjects.Count; i++){
            if(pushingObjects[i].gameObject.layer == 16){
                Debug.Log("REMOVED VIA LAYER");
                pushingObjects.Remove(pushingObjects[i].gameObject);
            }
            else{
                pushingObjects[i].transform.position = pushingObjects[i].transform.position + this.transform.right * (speed * Time.deltaTime);
                pushingObjects[i].transform.Rotate(new Vector3(0f, Time.deltaTime * spinAmount), Space.World);
            }
        }
    }
}


