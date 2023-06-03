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
 
    void Update() {
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
            
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2 && lane > Lane.Left) // Swipe Left
                {
                    lane--;
                    isMoving = true;
                }
                else if (touch.position.x > Screen.width / 2 && lane < Lane.Right) // Swipe Right
                {
                    lane++;
                    isMoving = true;
                }
            }
        }

        targetPosition = new Vector3(((int)lane - 1) * 5, transform.position.y, transform.position.z);
        
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, horizontalSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                isMoving = false;
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
