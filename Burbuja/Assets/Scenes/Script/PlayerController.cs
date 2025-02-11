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
    public float gravity;
    public float fallVelocity;
    public float jumpForce;

    public Camera mainCamara;
    private Vector3 movePlayer;
    private Vector3 camForward;
    private Vector3 camRight;
   
   public bool isOnSlope = false;
   private Vector3 hitNormal;
   public float slideVelocity;
   public float slopeForceDown;
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

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);
        Debug.Log(player.velocity.magnitude);

    }

    //funcion para determinar la direccion a la que mira la camara.
    void camDirection()
    {
        camForward = mainCamara.transform.forward;
        camRight = mainCamara.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    // funcion para las habilidades de nuestr jugador
    public void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }

    //funcion para la gravedad
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
        SlideDown();

    }

    public void SlideDown()
    {
        //isOnslopre sera verdavero siempre y cuando pase esto
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
        if (isOnSlope)
        {
            movePlayer.x += ((1f - hitNormal.y) *hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.z) *hitNormal.z) * slideVelocity;

            movePlayer.y += slopeForceDown;
        }
    }

    //DETECTA CUANDO COLICIONA CON OTRO OBJETO
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }
}
