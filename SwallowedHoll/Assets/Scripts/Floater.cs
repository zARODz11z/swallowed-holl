// Floater v0.0.2
// by Donovan Keith
//
// [MIT License](https://opensource.org/licenses/MIT)
 
using UnityEngine;
using System.Collections;
 
// Makes objects float up & down while gently spinning.
public class Floater : MonoBehaviour {
    // User Inputs
    public float degreesPerSecond = 15.0f;
    float amplitude = 0.35f;
    float frequency = .5f;
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    [SerializeField]
    bool independent;
 
    // Use this for initialization
    void Start () {
        if(independent){
            amplitude = Random.Range(.2f, .5f);
            frequency = Random.Range(.2f, .8f);
        }
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
     
    // Update is called once per frame
    void Update () {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
 
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = tempPos;
    }
}
