﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rb;
    public float playerSpeed;
    public float jumpForce;
    private float direction;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float deadLevel;

    public int extraJump;
    public int extraJumpValue;

    public bool fadeToNextLevel;
    public string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        // Setting fading to next level false as default
        fadeToNextLevel = false;

        // Making sure that the cursor isn't visible in-game
        Cursor.visible = false;

        extraJump = extraJumpValue;

        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Determine te direction in which the player is facing
        direction = Input.GetAxis("Horizontal");

        // Setting the player speed
        Rb.velocity = new Vector2(direction * playerSpeed, Rb.velocity.y);
    }

    private void Update()
    {
        Debug.Log(nextLevel);

        // Setting the extra (double) jump
        if (isGrounded)
        {
            extraJump = 1;
        }

        // Jumping mechanics
        if (Input.GetKeyDown(KeyCode.W) && extraJump > 0)
        {
            Rb.velocity = Vector2.up * jumpForce;
            extraJump--;
        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJump == 0 && isGrounded == true)
        {
            Rb.velocity = Vector2.up * jumpForce;
        }

        // Making sure whether or not the player fell off the map
        if (SceneManager.GetActiveScene().name != "Scenes/Center")
        {
            if (transform.position.y <= deadLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    // Loading the base scene when a key is earned
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Key")
        {
            if (SceneManager.GetActiveScene().name != "Scenes/Center")
            {
                nextLevel = "Scenes/Center";
                fadeToNextLevel = true;
            }
        }
    }
}
