using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10f; // Character movement speed
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Get horizontal input
        float moveVertical = Input.GetAxis("Vertical"); // Get vertical input

        Debug.Log(moveVertical);
        

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical); // Create movement vector
        body.AddForce(movement * speed); // Move the character
    }
}
