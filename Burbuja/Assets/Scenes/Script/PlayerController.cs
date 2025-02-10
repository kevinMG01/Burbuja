using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;

    public Camera mainCamara;
    private Vector3 camForward;
    private Vector3 camRight;
   
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        player.Move(movePlayer * Time.deltaTime);
        Debug.Log(player.velocity.magnitude);

    }

    void camDirection()
    {
        camForward = mainCamara.transform.forward;
        camRight = mainCamara.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void SetGravity()
    {
        if (player.isGrounded)//si el jugador esta tocando el suelo la gravedad sera sera fija = 9,8f 
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else // y si estamos en el aire aplicaremos la gravedad pero no sera constante sino que se sumara
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }

    }
}
