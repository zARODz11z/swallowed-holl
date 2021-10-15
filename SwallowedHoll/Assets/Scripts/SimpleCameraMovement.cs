using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleCameraMovement : MonoBehaviour
{
    [SerializeField]
    bool cursorLock = false;
    [SerializeField]
    Transform playerCamera = null;
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    [SerializeField, Range(0f, .5f)]
    float mouseSmoothTime = 0.03f;
    float pitch = 0f;
    
    [SerializeField, Range(0.1f, 10f)]
    float sens = 3.5f;


    void Awake() {
        if (!cursorLock) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
	void FixedUpdate()
    {
        UpdateMouseLook();
        
    }
    void UpdateMouseLook() {
    

        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        
            pitch -= currentMouseDelta.y * sens;
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            playerCamera.localEulerAngles = Vector3.right * pitch;

            transform.Rotate(Vector3.up * currentMouseDelta.x * sens);
            
    }
	
   




}
