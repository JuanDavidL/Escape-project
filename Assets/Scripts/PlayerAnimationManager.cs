using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerStats playerStats;
    CharacterController characterController;
    PlayerHealth playerHealth;

    Animator animator;

    //float Movement;
    bool isWalkingBackwards;
    bool isDashing;

    


    private void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>();
        characterController = GetComponent <CharacterController>();
        animator = GetComponent <Animator>();
        
        //playerHealth.OnHealthChanged += animator.SetTrigger("_Damaged");
        
        playerMovement.inputActionsForAnimator.Player.Jump.started += ctx => animator.SetTrigger("_Jump");
        playerMovement.inputActionsForAnimator.Player.Move.performed += ctx => UpdateSpeedValue();
        playerMovement.inputActionsForAnimator.Player.Move.performed += ctx => animator.SetBool("_IsWalking", true);
        playerMovement.inputActionsForAnimator.Player.Move.canceled += ctx => animator.SetBool("_IsWalking", false);
        playerMovement.inputActionsForAnimator.Player.Move.canceled += ctx => animator.SetFloat("_movementSpeed", 0f);
        playerMovement.inputActionsForAnimator.Player.Move.performed += ctx =>  isWalkingBackwards = (ctx.ReadValue<Vector2>().y < 0) ? true : false;
        playerMovement.inputActionsForAnimator.Player.Move.canceled += ctx =>  isWalkingBackwards = (ctx.ReadValue<Vector2>().y < 0) ? true : false;

        //playerMovement.inputActionsForAnimator.Player.Dash.started += ctx => isDashing = animator.SetTrigger("_Dash");

        playerMovement.inputActionsForAnimator.Player.Dash.performed += ctx => TryDash();
   
       
    }

    private void Update() {
        UpdateOnGround();
        CheckIfBackwards();
        UpdateSpeedValue();
      
    }

    void CheckIfBackwards()
    {        
        animator.SetBool("_IsWalkingBack", isWalkingBackwards);
    }

    void TryDash()
    {
        if(playerMovement.isDashingForAnimator && playerStats.CurrentDashForce >0) animator.SetTrigger("_Dash");
    }

    void UpdateOnGround()
    {
        if(characterController.isGrounded) animator.SetBool("_IsOnFloor", true);
        else animator.SetBool("_IsOnFloor", false);
    }

    void UpdateSpeedValue()
    {
        Debug.Log("CurrentMoveSpeed is " + playerStats.CurrentMoveSpeed);
        Debug.Log("currentBaseMove is " + playerStats.currentBaseMove);

        if (playerStats.CurrentMoveSpeed == playerStats.currentBaseMove)
        {
            animator.SetFloat("_movementSpeed", 1);
        }
        if (playerStats.CurrentMoveSpeed > playerStats.currentBaseMove)
        {
            animator.SetFloat("_movementSpeed", 2);
        }
    }


}
