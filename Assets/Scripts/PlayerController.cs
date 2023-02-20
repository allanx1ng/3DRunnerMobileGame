using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float forwardSpeed = 10f; // Character movement speed
    public float horizontalSpeed = 5f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = GetInput(); // Create movement vector
        rb.velocity = movement;
    }


    private Vector3 GetInput()
    {
        Vector3 movement = new Vector3(0f, 0f, 0f);

  
        CalculateMobileMovement(ref movement);
        CalculateComputerMovement(ref movement);
        

        return movement;
    }

    private void CalculateComputerMovement(ref Vector3 movement)
    {
        // Z-Axis (left, right)
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = horizontalSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement.x = -horizontalSpeed;
        }

        // X-Axis (forward, back)
        if (Input.GetKey(KeyCode.W))
        {
            movement.z = forwardSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.z = -forwardSpeed;
        }

    }

    private void CalculateMobileMovement(ref Vector3 movement)
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width / 2)
            {
                movement.x = -horizontalSpeed;
            } 
            else
            {
                movement.x = horizontalSpeed;
            }

        }
    }


}
