using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpPower = 10f;
    public float secondJumpPower = 10f;
    public Transform groundCheckposition;
    public float radius = 0.3f;
    public LayerMask layerGround;



    private Rigidbody myBody;
    private bool isGrounded;
    private bool playerJumped = false;
    private bool canDoubleJump = false;

    private PlayerAnimation playerAnim;

    private bool gameStarted;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }


    void FixedUpdate()
    {
        if (gameStarted)
        {
            MovePlayer();
            PlayerGrounded();
            PlayerJump();
        }

    }

    void MovePlayer()
    {
        myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
    }

    private void PlayerGrounded()
    {
        isGrounded = Physics.OverlapSphere(groundCheckposition.position, radius, layerGround).Length >0;

        if (isGrounded && playerJumped)
        {
            playerJumped = false;
            playerAnim.DidLand();
        }

        Debug.Log("Player is grounded");
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump) //double jump
        {
            canDoubleJump = false;
            myBody.AddForce(new Vector3(0, secondJumpPower, 0));
            Debug.Log("Jumped twice");
        }


        else if (Input.GetKeyUp(KeyCode.Space) && isGrounded) //single jump
        {
            playerAnim.DidJump();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canDoubleJump = true;
            Debug.Log("Jumped once");
        }

    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        gameStarted = true;
        playerAnim.PlayerRun();
       // playerAnim.PlayerIdle();

    }

}//class
