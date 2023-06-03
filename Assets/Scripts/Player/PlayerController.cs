using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    [Header("Player Properties")]
    public float forwardSpeed; 
    public float horizontalSpeed;
    
    private int health = 3;

    // Bounds the player's horizontal movement.
    public float leftBound = -7;
    public float rightBound = 7;

    public Animator animator;

    private Vector3 targetPosition;
    private Lane lane = Lane.Middle;
    private bool isMoving = false;

    private GameObject parent;

    public enum Lane
    {
        Left,
        Middle,
        Right
    }
    
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerManager.Instance.CurrentPlayer != null) {
            throw new Exception("There is already existing player in the scene!");
        } else {
            PlayerManager.Instance.SetCurrentPlayer(gameObject);
        }

        if (transform.parent == null) {
            Debug.Log("Please child the player object under an empty game object so that it could move.");
        }

        parent = transform.parent.gameObject;
        parent.tag = "Player";

        targetPosition = transform.position;
        
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
                Vector3 movement = new Vector3(deltaPosition.x, 0, 0) * horizontalSpeed * Time.deltaTime;

                // bound the player's movement
                Vector3 playerPosition = transform.position;

                if (movement.x + playerPosition.x > rightBound && movement.x > 0) {
                    movement.x = rightBound - playerPosition.x;
                }

                if (movement.x + playerPosition.x < leftBound && movement.x < 0) {
                    movement.x = leftBound - playerPosition.x;
                }

                // Move the player based on the movement and the speed
                parent.transform.Translate(movement, Space.World);
            }
        }
    }

    public void AddCoins(int coins) {
        CoinManager.Instance.AddCoins(coins);
    }

    private void movePlayerForward()
    {
        Vector3 movement = new Vector3(0, 0, 1);
        movement *= forwardSpeed * Time.deltaTime;
        parent.transform.Translate(movement, Space.World);
    }

    private void UpdatePlayerPosition() {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        UIManager.Instance.UpdateHearts(health);

        if (health <= 0)
        {
            UIManager.Instance.ToggleDeathMenu(true);   
        }

    }
    
    public float GetSpeed() {
        return forwardSpeed;
    }

}
