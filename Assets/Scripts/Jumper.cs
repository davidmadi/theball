using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float jumpForce = 25f;   // The force applied to the jump
    // Start is called before the first frame update
    public bool IsGrounded;       // Check if the player is grounded
    protected Rigidbody myRigidBody;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    void Awake()
    {
        this.myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Mathf.Abs(myRigidBody.velocity.y) < 0.5 && !this.IsGrounded)
        // {
        //     IsGrounded = true;
        // }
        //Control jump velocity
        if (myRigidBody.velocity.y < 0){
            myRigidBody.velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
        } else if (myRigidBody.velocity.y > 0) {
            myRigidBody.velocity += Vector3.up * Physics2D.gravity.y * (lowJumpMultiplier -1) * Time.deltaTime;
        }

        //Apply jump
        if (Input.GetMouseButtonUp(0) && IsGrounded) {
            IsGrounded = false;
            // Apply an upward force if the ball's velocity is below the threshold
            myRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply jump force
        }
    }

    public void Ground(){
        this.IsGrounded = true;
    }

}
