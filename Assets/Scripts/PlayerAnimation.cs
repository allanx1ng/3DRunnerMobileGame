using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator animator;
    private bool isJumping = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            animator.SetTrigger("jump");
            isJumping = true;    
        }
            
    }

    void FinishJumping()
    {
        isJumping = false;
    }

}
