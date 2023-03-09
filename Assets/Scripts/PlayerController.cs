using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float forwardSpeed; 
    public float horizontalSpeed;

    private GameObject parent;
    
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent == null) {
            Debug.Log("Please child the player object under an empty game object so that it could move.");
        }
        parent = transform.parent.gameObject;
        
        initializeCameraFollow();
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if (parent == null) return;
        movePlayerWithSwipe();
        movePlayerForward();
    }

    // Initializes the camera to follow the model component
    void initializeCameraFollow()
    {
        GameObject camera = GameObject.Find("Main Camera");
        Debug.Log(camera);
        camera.GetComponent<CameraFollow>().setTarget(gameObject);
    }

    // Moves the player as the user swipes the phone screen, relies on horizontalSpeed
    private void movePlayerWithSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // Get the delta position of the touch
                Vector2 deltaPosition = touch.deltaPosition;

                // Set the X and Z movement based on the delta position
                Vector3 movement = new Vector3(deltaPosition.x, 0, 0) * horizontalSpeed;

                // Move the player based on the movement and the speed
                parent.transform.Translate(movement * Time.deltaTime, Space.World);
            }
        }
    }

    private void movePlayerForward()
    {
        Vector3 movement = new Vector3(0, 0, 1);
        movement *= forwardSpeed * Time.deltaTime;
        parent.transform.Translate(movement, Space.World);
    }
    
    public float GetSpeed() {
        return forwardSpeed;
    }

}
