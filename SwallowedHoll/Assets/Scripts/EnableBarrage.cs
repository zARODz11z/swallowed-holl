using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBarrage : MonoBehaviour
{

    [SerializeField]
    GameObject sphere;
    Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = sphere.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.isBarraging){
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
