using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public LayerMask restartCondition;
    public bool restart;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Vector3 startPosition;

    //[SerializeField] AudioSource jumpSound;
   

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startPosition = transform.position;

        //SetSpawnPosition();
        
    }

    void SetSpawnPosition()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Scene1") { 
            // set the spawn position {
            Vector3 spawnPosition = new Vector3(41f, 60f, 0f);

            // set the player's position to the spawn position
            transform.position = spawnPosition;
        }

        else
        {
            Vector3 spawnPosition = new Vector3(0f, 188f, -9f);

            // set the player's position to the spawn position
            transform.position = spawnPosition;
        }




    }



    void Update()
    {
        //shoots a raycast to check if player is grounded or not 
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        

        if (grounded || restart)
            rb.drag = groundDrag;

        else
            rb.drag = 0;

        if (Input.GetKeyDown(jumpKey))
        {
            Debug.Log("Spacebar Pressed");
        }

        MyInput();
    }

    


    void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        //handles movement and jumping
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

        }
    }

    void MovePlayer()
    {
        // applies force to rb 
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    void Jump()
    {
        //applies an upword force to rb
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);   

        ///jumpSound.Play();
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}
