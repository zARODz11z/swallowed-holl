using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    //Menu images
    Image moveLeft;
    Image moveRight;
    Image moveUp;
    Image moveDown;
    Image jump;
    Image duck;
    Image sprint;
    Image interact;
    Image warp;
    Image throwItem;
    Image eat;
    Image diveCrouch;
    Image diveJump;

    // Start is called before the first frame update
    void Start()
    {
        //Temporary assignment until keybinding is done
        KeyCode leftKey = KeyCode.A;
        KeyCode rightKey = KeyCode.D;
        KeyCode upKey = KeyCode.W;
        KeyCode downKey = KeyCode.S;
        KeyCode jumpKey = KeyCode.Space;
        KeyCode duckKey = KeyCode.LeftControl;
        KeyCode runKey = KeyCode.LeftShift;
        KeyCode interKey = KeyCode.E;
        KeyCode warpKey = KeyCode.R;
        KeyCode throwKey = KeyCode.Mouse0;
        KeyCode eatKey = KeyCode.Mouse1;
        
        //Assign components
        moveLeft = GameObject.Find("WalkLeft").GetComponent<Image>();
        moveRight = GameObject.Find("WalkRight").GetComponent<Image>();
        moveUp = GameObject.Find("WalkForward").GetComponent<Image>();
        moveDown = GameObject.Find("WalkBack").GetComponent<Image>();
        jump = GameObject.Find("Jump").GetComponent<Image>();
        duck = GameObject.Find("Crouch").GetComponent<Image>();
        sprint = GameObject.Find("Sprint").GetComponent<Image>();
        interact = GameObject.Find("Interact").GetComponent<Image>();
        warp = GameObject.Find("WorldShift").GetComponent<Image>();
        throwItem = GameObject.Find("Throw").GetComponent<Image>();
        eat = GameObject.Find("Eat").GetComponent<Image>();
        diveCrouch = GameObject.Find("DiveCrouch").GetComponent<Image>();
        diveJump = GameObject.Find("DiveJump").GetComponent<Image>();


        //Set images to associated key sprite
        moveLeft.sprite = Resources.Load<Sprite>(leftKey.ToString());
        moveRight.sprite = Resources.Load<Sprite>(rightKey.ToString());
        moveUp.sprite = Resources.Load<Sprite>(upKey.ToString());
        moveDown.sprite = Resources.Load<Sprite>(downKey.ToString());
        jump.sprite = Resources.Load<Sprite>(jumpKey.ToString());
        duck.sprite = Resources.Load<Sprite>(duckKey.ToString());
        sprint.sprite = Resources.Load<Sprite>(runKey.ToString());
        interact.sprite = Resources.Load<Sprite>(interKey.ToString());
        warp.sprite = Resources.Load<Sprite>(warpKey.ToString());
        throwItem.sprite = Resources.Load<Sprite>(throwKey.ToString());
        eat.sprite = Resources.Load<Sprite>(eatKey.ToString());
        diveCrouch.sprite = Resources.Load<Sprite>(duckKey.ToString());
        diveJump.sprite = Resources.Load<Sprite>(jumpKey.ToString());
    }

}
