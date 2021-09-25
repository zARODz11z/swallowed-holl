using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBarrage : MonoBehaviour
{

    [SerializeField]
    GameObject sphere;
    FPSMovingSphere player;

    // Start is called before the first frame update
    void Start()
    {
        player = sphere.GetComponent<FPSMovingSphere>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.isBarraging);
        if (player.isBarraging){
            Debug.Log("Start Barraging");
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
