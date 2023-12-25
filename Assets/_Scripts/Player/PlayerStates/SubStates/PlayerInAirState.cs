using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool dashInput;

    //Checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;

    private bool coyoteTime;
    private bool walljumpCoyoteTime;
    private bool isJumping;

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(PlayerContext player, PlayerFSM playerFSM, PlayerData playerData, string animBoolName) : base(player, playerFSM, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded(); // true if grounded
        isTouchingWall = player.CheckIfTouchingWall(); // true if touching wall
        isTouchingWallBack = player.CheckIfTouchingWallBack(); // true if touching wall back
        isTouchingLedge = player.CheckIfTouchingLedge(); // true if touching ledge

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPostion(player.transform.position);
        }

        if(!walljumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.NormInputX; // it variable to make player can move in the air
        jumpInput = player.InputHandler.JumpInput; // variable to make player jump
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f) // when on the ground and yVelocity of player is less than 0.01
        {
            playerFSM.ChangeState(player.LandState);
        }
        else if(isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            playerFSM.ChangeState(player.LedgeClimbState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || walljumpCoyoteTime)) // when there is a jump input and touching wall
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            playerFSM.ChangeState(player.WallJumpState);
        }
        else if(jumpInput && player.JumpState.CanJump()) // when there is a jump input and still can jump
        {
            playerFSM.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge) // when player touching wall and there is a grab input
        {
            playerFSM.ChangeState(player.WallGrabState);
        }
        else if(isTouchingWall && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0) // when player is touching wall and there is xInput value to the wall
        {
            playerFSM.ChangeState(player.WallSlideState);
        }
        else if(dashInput && player.DashState.CheckIfCanDash())
        {
            playerFSM.ChangeState(player.DashState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput); // it make player can flip in the air
            player.SetVelocityX(playerData.movementVelocity * xInput); // it make player move in the air

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y); // calling parameter yVelocity from animator
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x)); // calling parameter xVelocity from animator but absolute
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    // allow player to coyote jump

    private void CheckWallJumpCoyoteTime()
    {
        if(walljumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            walljumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        walljumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void SetIsJumping() => isJumping = true;

    public void StopWallJumpCoyoteTime() => walljumpCoyoteTime = false;
}
