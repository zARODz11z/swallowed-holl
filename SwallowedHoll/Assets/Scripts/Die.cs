using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    //Author: Travis
    //[SerializeField]
    //float lifeMin = 5;
    //[SerializeField]
    //float lifeMax = 10;
    public DeathMenu deathScreen;

    //void Start()
    //{
        //Destroy(this.gameObject, Random.Range(lifeMin, lifeMax));
    //}

    //Author: Sandeep
    public void Death()
    {
      //pauses time so player cannot move
      Time.timeScale = 0;
      //activates the death menu
      deathScreen.gameObject.SetActive(true);
      //makes the cursor visible and unlocks it
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    }
}
